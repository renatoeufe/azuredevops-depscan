namespace depscan.Apis.AzureDevops.Search.Models.Search.Response;

public class Matches
{
    [JsonPropertyName("content")]
    public List<object> Content { get; set; }

    [JsonPropertyName("fileName")]
    public List<object> FileName { get; set; }
}