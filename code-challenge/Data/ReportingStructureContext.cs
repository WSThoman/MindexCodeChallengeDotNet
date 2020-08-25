using Microsoft.EntityFrameworkCore;

using challenge.Models;

namespace challenge.Data
{
    public class ReportingStructureContext : DbContext
    {
        public ReportingStructureContext( DbContextOptions<ReportingStructureContext> options) : base(options)
        {
        }

        public DbSet<ReportingStructure> ReportingStructures { get; set; }
    }
}
