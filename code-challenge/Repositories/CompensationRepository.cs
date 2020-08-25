using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using challenge.Data;
using challenge.Models;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        // Data members
        //
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        private readonly EmployeeContext _employeeContext;

        // Constructors
        //
        public CompensationRepository( ILogger<ICompensationRepository> logger,
                                       CompensationContext compensationContext,
                                       EmployeeContext employeeContext )
        {
            _compensationContext = compensationContext;
            _logger = logger;

            _employeeContext = employeeContext;
        }

        // Class methods
        //

        ///-------------------------------------------------------------------
        /// <summary>
        /// Adds a new 'Compensation' record to the database.
        /// </summary>
        /// <param name="compensation"></param>
        /// <remarks>
        /// The newly-added 'Compensation' object has the 'EmployeeId'
        /// property populated, and the 'Employee' property purposely set to
        /// 'null'.
        /// This is intended to not duplicate 'Employee' objects within the
        /// 'Compensation' table.
        /// Note, too, that multiple calls to 'Add()' will produce 'dupliate
        /// key' errors, and a full-featured application would protect
        /// against this condition.
        /// </remarks>
        /// <returns>
        /// Returns the newly-added 'Compensation' object
        /// </returns>
        ///-------------------------------------------------------------------
        public Compensation Add(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);

            return compensation;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the 'Compensation' object that matches the given Employee 'id'
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// For this GET call, the 'Employee' property which is stored in the
        /// 'Compensation' table as 'null' is populated here in the returned object
        /// with the corresponding Employee object from the Employees' table for
        /// convenience.
        /// For a completely populated direct reports hierarchy for this Employee
        /// object, please use the 'ReportingStructure' API call.
        /// </remarks>
        /// <returns>
        /// Returns the 'Compensation' object
        /// </returns>
        ///-------------------------------------------------------------------
        public Compensation GetById(string id)
        {
            Compensation compensation =
                _compensationContext.Compensations
                .SingleOrDefault( c => c.EmployeeId == id );

            compensation.Employee = _employeeContext.Employees
                                    .Include( e => e.DirectReports )
                                    .SingleOrDefault( e => e.EmployeeId == id );
            return compensation;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
