using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using challenge.Data;
using challenge.Models;

namespace challenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        // Data members
        //
        private readonly ILogger<IReportingStructureRepository> _logger = null;

        private readonly EmployeeRepository _employeeRepository = null;

        private ReportingStructure _reportingStructure = null;
	    private int _numDirectReports = 0;

        // Constructors
        //
        public ReportingStructureRepository( ILogger<IReportingStructureRepository> logger,
                                             ILogger<IEmployeeRepository> loggerEmp,
                                             EmployeeContext employeeContext )
        {
            _logger = logger;
       
            _employeeRepository =
                new EmployeeRepository( loggerEmp, employeeContext );
        }

        // Class methods
        //
        public ReportingStructure GetById( string id )
        {
            _logger.LogDebug($"Received ReportingStructureReportingStructure GetById request for '{id}'");

            var employee = _employeeRepository.GetById( id );

            if (employee == null)
            {
                return null;
            }

            // Init the recursion members
            //
            _reportingStructure = new ReportingStructure();
            _reportingStructure.Employee = employee;

            _numDirectReports = 0;

            // Recursively traverse the data for each employee's 'direct reports'
            //
            if (employee.DirectReports != null)
            {
                List<Employee> dirReports = GetDirectReportsInfo( employee.DirectReports );

                _reportingStructure.Employee.DirectReports = dirReports;

                // Save the number of 'direct reports' to our structure
                //
                _logger.LogDebug( $"Final _numDirectReports: '{_numDirectReports}'" );

                _reportingStructure.NumberOfReports = _numDirectReports;
            }

            // Return result to caller
            //
            return _reportingStructure;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Recursively fill the 'direct reports' info from the in-memory database
        /// </summary>
        /// <param name="dirReportsList"></param>
        /// <returns>
        /// List of 'Employee' objects who are 'direct reports'
        /// </returns>
        ///-------------------------------------------------------------------
        private List<Employee> GetDirectReportsInfo( List<Employee> dirReportsList )
        {
            foreach (Employee drEmployee in dirReportsList)
            {
        	    Employee infoEmployee = _employeeRepository.GetById( drEmployee.EmployeeId );

                if (infoEmployee == null)
                {
                    throw new Exception( "Invalid EmployeeId: " + drEmployee.EmployeeId );
                }
            
        	    drEmployee.FirstName  = infoEmployee.FirstName;
        	    drEmployee.LastName   = infoEmployee.LastName;
        	    drEmployee.Position   = infoEmployee.Position;
        	    drEmployee.Department = infoEmployee.Department;
  
        	    // Each Employee in the list is a 'direct report', so increment the count
        	    //
        	    _numDirectReports++;
        	
        	    // Direct reports for this 'info' employee
        	    //
                if (infoEmployee.DirectReports != null)
                {
            	    List<Employee> subDirReports = GetDirectReportsInfo( infoEmployee.DirectReports );
            	
	        	    drEmployee.DirectReports = subDirReports;
                }
            }
        
            return dirReportsList;
        }    
    }
}
