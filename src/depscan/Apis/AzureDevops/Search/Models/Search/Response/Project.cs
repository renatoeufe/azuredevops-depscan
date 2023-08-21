namespace depscan.Apis.AzureDevops.Search.Models.Search.Response;

public class Project
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}