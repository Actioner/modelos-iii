using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.Domain;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class RunRepository : NHibernateRepository<Run>, IRunRepository
    {
        public IEnumerable<Run> GetByDates(DateTime? start, DateTime? end)
        {
            var query = Session.QueryOver<Run>();

            if (start != null)
            {
                query.Where(r => start < r.RunOn);
                if (end != null)
                {
                    query.And(r => r.RunOn < end);
                }
            }
            else if (end != null)
            {
                query.Where(r => r.RunOn < end);
            }

            return query.Future<Run>();
        }

        public IEnumerable<Run> GetByScenario(Scenario scenario)
        {
            Check.Require(scenario != null);

            return Session.QueryOver<Run>()
                .Where(r => r.Scenario.Id == scenario.Id)
                .Future<Run>();
        }

        public IEnumerable<Run> GetByScenarioAndDates(Scenario scenario, DateTime? start, DateTime? end)
        {
            Check.Require(scenario != null);

            var query = Session.QueryOver<Run>()
                .Where(r => r.Scenario.Id == scenario.Id);

            if (start != null)
            {
                query.And(r => start < r.RunOn);
            }

            if (end != null) 
            {
                query.And(r => r.RunOn < end);
            }

            return query.Future<Run>();
        }
    }
}
