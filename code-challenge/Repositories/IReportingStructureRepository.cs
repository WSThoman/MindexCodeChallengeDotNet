using challenge.Models;

namespace challenge.Repositories
{
    public interface IReportingStructureRepository
    {
        ReportingStructure GetById( string id );
    }
}
