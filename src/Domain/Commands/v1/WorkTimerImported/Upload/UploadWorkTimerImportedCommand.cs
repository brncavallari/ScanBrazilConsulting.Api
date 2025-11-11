namespace Domain.Commands.v1.WorkTimerImported.Upload;

public class UploadWorkTimerImportedCommand : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public DateTime CreatedAt { get; set; }
    public string FileName { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
}

