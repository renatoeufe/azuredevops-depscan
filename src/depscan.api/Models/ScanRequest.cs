namespace depscan.api.Models
{
    public class ScanRequest
    {
        public string User { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string Feed { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public string Repo { get; set; } = string.Empty;
    }
}