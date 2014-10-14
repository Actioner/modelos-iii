using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class MultiplexRepository : NHibernateRepository<Multiplex>, IMultiplexRepository
    {
        public Multiplex GetByName(string name)
        {
            return Session.QueryOver<Multiplex>()
                .Where(m => m.Name == name)
                .SingleOrDefault();
        }
    }
}
