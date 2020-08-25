﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using challenge.Models;
using challenge.Services;

namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        // Data members
        //
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        // Constructors
        //
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        // Class methods
        //
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(string id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(string id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Received employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById( id );

            if (existingEmployee == null)
            {
                return NotFound();
            }

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
    }
}
