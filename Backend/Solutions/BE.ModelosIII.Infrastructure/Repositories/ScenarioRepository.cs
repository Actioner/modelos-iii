using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class ScenarioRepository : NHibernateRepository<Scenario>, IScenarioRepository
    {
        public Scenario GetByName(string name)
        {
            return Session.QueryOver<Scenario>()
                .Where(m => m.Name == name)
                .SingleOrDefault();
        }
    }
}
