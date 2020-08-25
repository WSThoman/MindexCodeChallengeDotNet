using Microsoft.EntityFrameworkCore;

using challenge.Models;

namespace challenge.Data
{
    public class EmployeeContext : DbContext
    {
        // Constructors
        //
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        // Properties
        //
        public DbSet<Employee> Employees { get; set; }
    }
}
