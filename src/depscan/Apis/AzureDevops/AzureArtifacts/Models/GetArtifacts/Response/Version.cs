namespace depscan.Apis.AzureDevops.AzureArtifacts.Models.GetArtifacts.Response;

public class Version
{
    [JsonPropertyName("version")] public string VersionNumber { get; set; }
    [JsonPropertyName("isLatest")] public bool IsLatest { get; set; }
}