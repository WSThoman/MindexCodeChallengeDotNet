using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using challenge.Services;

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
    public class ReportingStructureController : Controller
    {
        // Data members
        //
        private readonly ILogger _logger = null;
        private readonly IReportingStructureService _reportingStructureService = null;

        // Constructors
        //
        public ReportingStructureController( ILogger<ReportingStructureController> logger,
                                             IReportingStructureService reportingStructureService )
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        // Class methods
        //
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById( string id )
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var reportingStructure = _reportingStructureService.GetById( id );

            if (reportingStructure == null)
            {
                return NotFound();
            }

            return Ok( reportingStructure );
        }
    }
}
