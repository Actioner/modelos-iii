using System;
using System.Collections.Generic;
using System.Linq;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.Domain;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class ScreeningRepository : NHibernateRepository<Screening>, IScreeningRepository
    {
        public IList<Screening> GetForMovie(Movie movie, DateTime start, DateTime end)
        {
            return Session.QueryOver<Screening>()
                .Where(sc => sc.Movie.Id == movie.Id && sc.StartDate >= start && sc.StartDate < end)
                .List();
        }

        public IList<Screening> GetByMultiplexAndScreenAndDate(Multiplex multiplex, Screen screen, DateTime date)
        {
            var startDate = date.Date;
            var nextDate = date.Date.AddDays(1);
            var query = Session.QueryOver<Screening>();

            if (multiplex != null)
            {
                query.JoinQueryOver(sc => sc.Screen)
                    .And(scr => scr.Multiplex.Id == multiplex.Id);
            }

            if (screen != null)
            {
                query.And(sc => sc.Screen.Id == screen.Id);
            }

            if (startDate != DateTime.MinValue)
            {
                query.And(sc => sc.StartDate >= startDate && sc.StartDate < nextDate);
            }

            return query.List();
        }

        public IList<Screening> GetByScreenAndDateRange(Screen screen, DateTime start, DateTime end)
        {
            Check.Require(screen != null);
            Check.Require(start != DateTime.MinValue);
            Check.Require(end != DateTime.MinValue);

            var query = Session.QueryOver<Screening>()
                .Where(scr => scr.Screen.Id == screen.Id)
                .And(scr => scr.StartDate < end);

            var partialResult = query.List();

            return partialResult.Where(scr => scr.EndDate > start).ToList();
        }
    }
}
