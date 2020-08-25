using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using challenge.Models;
using challenge.Services;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        // Data members
        //
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        // Constructors
        //
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        // Class methods
        //
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation );
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(string id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var employee = _compensationService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
