namespace depscan.Apis.AzureDevops.Search.Models.Search.Response;

public class Version
{
    [JsonPropertyName("branchName")] public string BranchName { get; set; }

    [JsonPropertyName("changeId")] public string ChangeId { get; set; }
}