using Microsoft.Extensions.Logging;

using challenge.Models;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        // Data members
        //
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        // Constructors
        //
        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        // Class methods
        //
        public Employee Create(Employee employee)
        {
            if (employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the Employee' object for the given 'id'
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Note that this method purposely returns only the top-level
        /// 'DirectReports' for the given Employee.
        /// For a completely populated direct reports hierarchy, please use
        /// the 'ReportingStructure' API call.
        /// </remarks>
        /// <returns>
        /// Returns the Employee' object
        /// </returns>
        ///-------------------------------------------------------------------
        public Employee GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if (originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
               
                if (newEmployee != null)
                {
                    // Ensure the original has been removed, otherwise EF will complain that
                    // another entity w/ same id already exists
                    //
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                
                    // Overwrite the new id with previous employee id
                    //
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }

                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }
    }
}
