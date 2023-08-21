namespace depscan.Apis.AzureDevops.AzureArtifacts.Models.GetArtifacts.Response;

public class GetArtifactsResponse
{
    [JsonPropertyName("value")] public List<Value> Value { get; set; }
}