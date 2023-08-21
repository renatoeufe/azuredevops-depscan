namespace depscan.Apis.NuGet.Models.GetPackage
{
    public class GetPackageResponse
    {
        [JsonPropertyName("versions")]
        public List<string> Versions { get; set; }
    }
}