namespace depscan.Apis.AzureDevops.Search.Models.Search.Response;

public class Repository
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}