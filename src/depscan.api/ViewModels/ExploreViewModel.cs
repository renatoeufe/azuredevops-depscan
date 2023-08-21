using depscan.api.Models;

namespace depscan.api.ViewModels
{
    public class ExploreViewModel
    {
        public ScanRequest Request { get; set; } = new();
        public List<FilesByPackageSummary> Summary { get; set; } = new();

        public int CountUpdated => Summary.Count(x => x.AllUpdated);
        public int CountNotUpdated => Summary.Count(x => !x.AllUpdated);
    }

    public class FilesByPackageSummary
    {
        public string PackageName { get; set; } = string.Empty;
        public List<FilesByPackage> FilesByPackage { get; set; } = new();

        public bool AllUpdated => FilesByPackage.All(x => x.Updated);
        public string Color => AllUpdated ? "#20c997" : "#dc3545";
        public string LastVersion => FilesByPackage.FirstOrDefault()?.LastVersion ?? "?";
        public string StableVersion => FilesByPackage.FirstOrDefault()?.StableVersion ?? "?";

        public bool HaveMismatch =>
            FilesByPackage.Select(x => x.InstalledVersion).Distinct().Count() > 1;
    }

    public class FilesByPackage
    {
        public string Path { get; set; } = string.Empty;
        public string InstalledVersion { get; init; } = string.Empty;
        public string LastVersion { get; set; } = string.Empty;
        public string StableVersion { get; set; } = string.Empty;

        public bool Updated => InstalledVersion.Equals(LastVersion, StringComparison.InvariantCulture) ||
                               InstalledVersion.Equals(StableVersion, StringComparison.InvariantCulture);

        public string Color => Updated ? "#20c997" : "#dc3545";
        public string Class => Updated ? "bg-success" : "bg-danger";
    }
}