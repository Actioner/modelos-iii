using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class ScreenRepository : NHibernateRepository<Screen>, IScreenRepository
    {
        public IList<Screen> GetByMultiplex(Multiplex multiplex)
        {
            return Session.QueryOver<Screen>()
                .Where(s => s.Multiplex.Id == multiplex.Id)
                .List();
        }

        public Screen GetByMultiplexAndName(Multiplex multiplex, string name)
        {
            return Session.QueryOver<Screen>()
                .Where(s => s.Multiplex.Id == multiplex.Id && s.Name == name)
               .SingleOrDefault();
        }
    }
}
