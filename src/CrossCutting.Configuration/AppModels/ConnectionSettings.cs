using System.Text.Json.Serialization;

namespace CrossCutting.Configuration.AppModels;
public sealed class ConnectionSettings
{
    [JsonPropertyName("connectionString")]
    public string ConnectionString { get; set; } = string.Empty;

    [JsonPropertyName("databaseName")]
    public string DatabaseName { get; set; } = string.Empty;
}
