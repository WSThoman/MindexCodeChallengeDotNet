using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using challenge.Data;
using challenge.Models;

namespace challenge.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // Data members
        //
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        // Constructors
        //

        // NOTE: The original code challenge had 'EmployeeRepository' misspelled as 'EmployeeRespository'
        public EmployeeRepository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        // Class methods
        //
        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            
            _employeeContext.Employees.Add(employee);

            return employee;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the requested 'Employee' object by the given 'id'.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// NOTE: The '.Include' clause in the call explicitly
        /// fills the 'DirectReports' list in the returned 'Employee'
        /// object.
        /// This was a purposeful omission for this coding challenge.
        /// </remarks>
        /// <returns>
        /// Returns the requested 'Employee' object.
        /// </returns>
        ///-------------------------------------------------------------------
        public Employee GetById(string id)
        {
            return _employeeContext.Employees
                       .Include( e => e.DirectReports )
                       .SingleOrDefault( e => e.EmployeeId == id );
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
