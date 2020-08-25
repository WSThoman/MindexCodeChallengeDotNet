using Microsoft.Extensions.Logging;

using challenge.Models;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        // Data members
        //
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        // Constructors
        //
        public CompensationService( ILogger<CompensationService> logger,
                                    ICompensationRepository compensationRepository )
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        // Class methods
        //
        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }
    }
}
