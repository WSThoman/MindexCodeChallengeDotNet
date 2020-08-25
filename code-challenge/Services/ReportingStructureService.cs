using Microsoft.Extensions.Logging;

using challenge.Models;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        // Data members
        //
        private readonly ILogger<ReportingStructureService> _logger;

        private readonly IReportingStructureRepository _reportingStructureRepository;

        // Constructors
        //
        public ReportingStructureService( ILogger<ReportingStructureService> logger,
                                          IReportingStructureRepository reportingStructureRepository )
        {
            _logger = logger;
            _reportingStructureRepository = reportingStructureRepository;
        }

        // Class methods
        //
        public ReportingStructure GetById( string id )
        {
            _logger.LogDebug($"Received ReportingStructure GetById request for '{id}'");

            if ( ! string.IsNullOrEmpty( id ))
            {
                return _reportingStructureRepository.GetById( id );
            }

            return null;
        }
    }
}
