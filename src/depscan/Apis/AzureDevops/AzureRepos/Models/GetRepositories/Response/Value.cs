namespace depscan.Apis.AzureDevops.AzureRepos.Models.GetRepositories.Response;

public class Value
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("project")] public Project Project { get; set; }
    [JsonPropertyName("remoteUrl")] public string RemoteUrl { get; set; }
    [JsonPropertyName("defaultBranch")] public string DefaultBranch { get; set; }
}