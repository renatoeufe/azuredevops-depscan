using depscan.api.Models;
using depscan.api.ViewModels;
using depscan.Apis;
using depscan.Application;
using depscan.Application.Collector;
using depscan.Application.Scanner;
using depscan.Application.VersionChecker;

namespace depscan.api.Application.UseCases
{
    public class GetSummaryUseCase
    {
        public async Task<List<FilesByPackageSummary>> Execute(ScanRequest request)
        {
            var credentials = new Credentials(request.User, request.AccessToken, request.Organization);
            var scanParameters = new ScanParameters(credentials, request.Feed, request.Project, request.Repo);
            var clients = new Clients(request.Organization);

            IPackageCollector packageCollector = new PackageCollector(clients, scanParameters);
            IPackageVersionChecker packageVersionChecker = new PackageVersionChecker(clients, scanParameters);
            IScanner scanner = new DefaultScanner(packageCollector, packageVersionChecker);

            var projectFiles = await scanner.Run().ConfigureAwait(false);
            var summary = new List<FilesByPackageSummary>();

            var packageNames = packageVersionChecker
                .GetCachedPackageNames()
                .OrderBy(x => x);

            foreach (var packageName in packageNames)
            {
                var item = new FilesByPackageSummary
                {
                    PackageName = packageName
                };

                var projectsWithPackage =
                    projectFiles
                        .Where(p => p.PackageReferences.Any(r => r.Name == packageName));

                foreach (var projectFileInfo in projectsWithPackage)
                {
                    var packageFileInfo =
                        projectFileInfo.PackageReferences
                            .FirstOrDefault(r => r.Name == packageName);

                    if (packageFileInfo == null) continue;

                    item.FilesByPackage.Add(new FilesByPackage
                    {
                        Path = Path.GetFileName(projectFileInfo.Path),
                        InstalledVersion = packageFileInfo.InstalledVersion,
                        LastVersion = packageFileInfo.LastVersion,
                        StableVersion = packageFileInfo.StableVersion
                    });
                }

                summary.Add(item);
            }

            return summary;
        }
    }
}