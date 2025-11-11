using Microsoft.Extensions.Configuration;

namespace CrossCutting.Configuration;

public static class AppSettingsConfiguration
{
    internal static class AppSettingsLoader
    {
        public static AppSettings Load()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var settings = new AppSettings();
            configuration.Bind(settings);

            return settings;
        }
    }
}
