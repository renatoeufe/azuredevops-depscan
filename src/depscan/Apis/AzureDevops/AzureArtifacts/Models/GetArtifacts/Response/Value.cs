namespace depscan.Apis.AzureDevops.AzureArtifacts.Models.GetArtifacts.Response;

public class Value
{
    [JsonPropertyName("normalizedName")] public string NormalizedName { get; set; }
    [JsonPropertyName("protocolType")] public string ProtocolType { get; set; }
    [JsonPropertyName("versions")] public List<Version> Versions { get; set; }
}