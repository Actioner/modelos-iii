using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class PriceRepository : NHibernateRepository<Price>, IPriceRepository
    {
        public Price GetGeneralPrice()
        {
            return Session.QueryOver<Price>()
              .Where(m => m.Type == PriceType.General)
              .SingleOrDefault();
        }
    }
}
