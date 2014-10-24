using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class PopulationRepository : NHibernateRepository<Population>, IPopulationRepository
    {
        public Population FindBestByRun(Run run)
        {
            Population population = null;

            return Session.QueryOver<Population>(() => population)
                .Left.JoinQueryOver(p => p.Generation)
                .Left.JoinQueryOver(g => g.Run)
                .Where(r => r.Id == run.Id)
                .OrderBy(() => population.Fitness).Desc
                .Take(1)
                .SingleOrDefault();
        }
    }
}
