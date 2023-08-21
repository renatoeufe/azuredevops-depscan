using System.Text.Json;
using depscan.api.Application.UseCases;
using depscan.api.Models;
using depscan.api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace depscan.api.Controllers.Ui
{
    public class ExploreController : Controller
    {
        private readonly GetSummaryUseCase _useCase = new();
        private readonly ILogger<ExploreController> _logger;

        public ExploreController(ILogger<ExploreController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            _logger.LogTrace("callled ExploreController.Index");

            var vm = new ExploreViewModel
            {
                Request = new ScanRequest
                {
                    Organization = Environment.GetEnvironmentVariable("organization") ?? string.Empty,
                    Feed = Environment.GetEnvironmentVariable("feed") ?? string.Empty,
                    AccessToken = "",
                    Project = "",
                    Repo = "",
                    User = Environment.GetEnvironmentVariable("user") ?? string.Empty
                }
            };

            _logger.LogTrace("returning view ExploreController.Index");

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ScanRequest request)
        {
            _logger.LogTrace("callled ExploreController.Index");

            try
            {
                var summary =
                    await _useCase
                        .Execute(request)
                        .ConfigureAwait(false);

                var vm = new ExploreViewModel
                {
                    Request = request,
                    Summary = summary
                };

                return View(vm);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ExploreController.Scan");
                return RedirectToAction("Index");
            }
        }
    }
}