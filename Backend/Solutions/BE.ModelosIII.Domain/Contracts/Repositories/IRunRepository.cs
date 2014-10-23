using System;
using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IRunRepository : IRepository<Run>
    {
        IEnumerable<Run> GetByDates(DateTime? start, DateTime? end);
        IEnumerable<Run> GetByScenario(Scenario scenario);
        IEnumerable<Run> GetByScenarioAndDates(Scenario scenario, DateTime? start, DateTime? end);
    }
}
