namespace depscan.Apis.AzureDevops.AzureRepos.Models.GetRepositories.Response;

public class Project
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("state")] public string State { get; set; }
}