using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class PromotionRepository : NHibernateRepository<Promotion>, IPromotionRepository
    {
        public IList<Promotion> GetAvailable(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            var partial = Session.QueryOver<Promotion>()
               .Where(p => p.Active && p.StartDate <= date && p.EndDate >= date)
               .List();

            return partial.Where(p => p.Days.Contains(dayOfWeek)).ToList();
        }
    }
}
