using Microsoft.Extensions.Configuration;

namespace CrossCutting.Configuration;

public static class AppSettingsConfiguration
{
    internal static class AppSettingsLoader
    {
        public static AppSettings Load()
        {
            // Identifica o ambiente atual (Development, Production, etc.)
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            // Cria o ConfigurationBuilder para ler o arquivo JSON
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Cria uma instância e faz o binding dos valores
            var settings = new AppSettings();
            configuration.Bind(settings);

            return settings;
        }
    }
}
