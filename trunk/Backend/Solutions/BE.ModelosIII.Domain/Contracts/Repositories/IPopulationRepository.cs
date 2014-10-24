using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IPopulationRepository : IRepository<Population>
    {
        Population FindBestByRun(Run run);
    }
}
