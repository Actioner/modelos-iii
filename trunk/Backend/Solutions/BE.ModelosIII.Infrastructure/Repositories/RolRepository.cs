using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class RolRepository : NHibernateRepository<Role>, IRolRepository
    {
        public Role GetByName(string name)
        {
            return Session.QueryOver<Role>().Where(u => u.Name == name).SingleOrDefault();
        }
    }
}
