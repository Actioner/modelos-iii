using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class UserRepository : NHibernateRepository<User>, IUserRepository
    {
        public virtual User GetByEmail(string email)
        {
            return Session.QueryOver<User>().Where(u => u.Email == email).SingleOrDefault();
        }

        public IEnumerable<User> FindyByRol(Role role)
        {
            return Session.QueryOver<User>().Where(u => u.Role == role).Future<User>();
        }
    }
}
