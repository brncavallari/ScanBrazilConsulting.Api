namespace Domain.Service.v1;
public class ProtocolService
{
    public static string GenerateProtocol(string userEmail)
    {
        var userInitials = GetUserInitials(userEmail);
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

        return $"SCAN-{userInitials}-{timestamp}";
    }
    private static string GetUserInitials(string email)
    {
        try
        {
            var userName = email.Split('@')[0];

            if (userName.Contains('.'))
            {
                var parts = userName.Split('.');
                if (parts.Length >= 2)
                {
                    var first = parts[0].Length > 0 ? parts[0][0] : 'U';
                    var second = parts[1].Length > 0 ? parts[1][0] : 'S';
                    return $"{first}{second}".ToUpper();
                }
            }

            if (userName.Length >= 2)
                return userName.Substring(0, 2).ToUpper();

            return userName.Length == 1
                ? $"{userName}X".ToUpper()
                : "US";
        }
        catch
        {
            return "US";
        }
    }
}
