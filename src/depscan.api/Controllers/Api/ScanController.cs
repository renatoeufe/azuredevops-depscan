using depscan.api.Application.UseCases;
using depscan.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace depscan.api.Controllers.Api
{
    [ApiController]
    [Route("scans")]
    public class ScanController : ControllerBase
    {
        private readonly ILogger<ScanController> _logger;
        private readonly GetSummaryUseCase _useCase = new();

        public ScanController(ILogger<ScanController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "")]
        public async Task<IActionResult> RequestScan(ScanRequest request)
        {
            _logger.LogDebug("ScanController.RequestScan called");

            var summary =
                await _useCase
                    .Execute(request)
                    .ConfigureAwait(false);

            return Ok(summary);
        }
    }
}