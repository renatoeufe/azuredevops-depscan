namespace depscan.Apis.AzureDevops.AzureRepos.Models.GetRepositories.Response;

public class GetRepositoriesResponse
{
    [JsonPropertyName("count")] public int Count { get; set; }
    [JsonPropertyName("value")] public List<Value> Value { get; set; }
}