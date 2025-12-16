namespace Domain.Builder.v1;

public static class WorkTimersBuilder
{
    #region FileImport
    public static async Task<List<WorkTimerInformation>> FileImportAsync(
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        ValidateFile(file);

        using var workbook = new XLWorkbook(await ToMemoryStreamAsync(file, cancellationToken));
        var worksheet = workbook.Worksheets.FirstOrDefault()
            ?? throw new Exception("A planilha enviada está vazia ou não contém abas.");

        var usedRange = worksheet.RangeUsed()
            ?? throw new Exception("Não há dados na planilha.");

        const int headerRow = 1;
        var (firstCol, lastCol, lastRow) = GetSheetDimensions(worksheet);

        var headers = GetHeaders(worksheet, headerRow, firstCol, lastCol);
        var props = typeof(WorkTimerInformation).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var result = new List<WorkTimerInformation>();

        for (int row = headerRow + 1; row <= lastRow; row++)
        {
            var entity = MapRowToEntity(worksheet, row, headers, props, firstCol, lastCol);

            if (!IsEntityEmpty(entity, props))
                result.Add(entity);
        }

        return result;
    }

    private static void ValidateFile(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("Arquivo não enviado ou está vazio.", nameof(file));
    }

    private static async Task<MemoryStream> ToMemoryStreamAsync(IFormFile file, CancellationToken token)
    {
        var ms = new MemoryStream();
        await file.CopyToAsync(ms, token);
        ms.Position = 0;
        return ms;
    }

    private static (int firstCol, int lastCol, int lastRow) GetSheetDimensions(IXLWorksheet sheet)
    {
        return (
            sheet.FirstColumnUsed().ColumnNumber(),
            sheet.LastColumnUsed().ColumnNumber(),
            sheet.LastRowUsed().RowNumber()
        );
    }

    private static List<string> GetHeaders(IXLWorksheet sheet, int headerRow, int firstCol, int lastCol)
    {
        return Enumerable
            .Range(firstCol, lastCol - firstCol + 1)
            .Select(col => sheet.Cell(headerRow, col).GetString().Trim())
            .ToList();
    }

    private static WorkTimerInformation MapRowToEntity(
        IXLWorksheet sheet,
        int row,
        List<string> headers,
        PropertyInfo[] props,
        int firstCol,
        int lastCol)
    {
        var entity = new WorkTimerInformation();

        for (int col = firstCol; col <= lastCol; col++)
        {
            var header = NormalizeHeader(headers[col - firstCol]);
            if (string.IsNullOrEmpty(header))
                continue;

            var value = sheet.Cell(row, col).GetString().Trim();
            var prop = FindMatchingProperty(props, header);

            prop?.SetValue(entity, value);
        }

        return entity;
    }

    private static string NormalizeHeader(string header)
    {
        return header?
            .Replace(" ", "")
            .Replace("/", "")
            .Replace("Número", "Numero")
            .Trim();
    }

    private static PropertyInfo FindMatchingProperty(PropertyInfo[] props, string header)
    {
        return props.FirstOrDefault(p =>
            string.Equals(p.Name, header, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(
                p.GetCustomAttribute<BsonElementAttribute>()?.ElementName,
                header,
                StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsEntityEmpty(WorkTimerInformation entity, PropertyInfo[] props)
    {
        return props.All(p => string.IsNullOrWhiteSpace(p.GetValue(entity)?.ToString()));
    }

    #endregion

    #region CalculateExtraHours
    public static double CalculateExtraHours(IEnumerable<WorkTimerInformation> items)
    {
        var total = items.Select(x => double.TryParse(x.CompletedWork, out var v) ? v : 0).Sum();
        return total - 160;
    }
    #endregion

    #region Regex
    public static (string Name, string Email) ExtractNameAndEmail(string input)
    {
        var email = Regex.Match(input, @"<([^>]+)>").Groups[1].Value.Trim();
        var name = input.Split('<')[0].Trim();
        return (name, email);
    }
    #endregion

    #region MemoryStream
    public static MemoryStream ToMemoryStream(IFormFile file)
    {
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }
    #endregion

    #region Validation
    public static void SetRamark(UserTimerInformation userTimer, double hour, string description, string name)
    {
        Remark remark = new() { Value = hour, Description = description, UpdateAt = DateTime.UtcNow, UserName = name };

        if (userTimer.Remarks is null)
            userTimer.Remarks = [remark];
        else
            userTimer.Remarks.Add(remark);
    }

    public static IList<Remark> CreateRemark(string description, double hour, string name)
    {
        var remark = new Remark()
        {
            Description = description,
            Value = hour,
            UpdateAt = DateTime.UtcNow,
            UserName = name
        };

        return [remark];
    }
    #endregion

}