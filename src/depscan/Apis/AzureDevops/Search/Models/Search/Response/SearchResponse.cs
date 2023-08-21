namespace depscan.Apis.AzureDevops.Search.Models.Search.Response;

public class SearchResponse
{
    [JsonPropertyName("count")]
    public long Count { get; set; }

    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }

    [JsonPropertyName("infoCode")]
    public long InfoCode { get; set; }

    [JsonPropertyName("facets")]
    public Facets Facets { get; set; }
}