using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using NHibernate.Criterion;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class MovieRepository : NHibernateRepository<Movie>, IMovieRepository
    {
        public IList<Movie> GetByTitle(string title)
        {
            return Session.QueryOver<Movie>().Where(u => u.Title.Equals(title,StringComparison.InvariantCultureIgnoreCase)).List();
        }

        public IList<Movie> GetByOriginalTitle(string title)
        {
            return Session.QueryOver<Movie>().Where(u => u.OriginalTitle.Equals(title, StringComparison.InvariantCultureIgnoreCase)).List();
        }

        public IList<Movie> FindAllWithScreening(DateTime dateFrom, DateTime dateTo)
        {
            Movie movie = null;
            var startDate = dateFrom.Date;
            var nextDate = dateTo.Date.AddDays(1);

            return Session.QueryOver(() => movie)
                .WithSubquery
                .WhereExists(QueryOver.Of<Screening>()
                    .Where(sc => sc.Movie.Id == movie.Id && (sc.StartDate >= startDate && sc.StartDate < nextDate))
                    .Select(sc => sc.Id))
                .List();
        }

        public IList<Movie> FindAllWithScreeningAt(Multiplex multiplex, DateTime dateFrom, DateTime dateTo)
        {
            Movie movie = null;
            Screening screening = null;
            var startDate = dateFrom.Date;
            var nextDate = dateTo.Date.AddDays(1);

            return Session.QueryOver(() => movie)
                .WithSubquery
                .WhereExists(QueryOver.Of(() => screening)
                    .Left.JoinQueryOver(sc => sc.Screen)
                    .Left.JoinQueryOver(sc => sc.Multiplex)
                    .Where(m => m.Id == multiplex.Id && screening.Movie.Id == movie.Id && (screening.StartDate >= startDate && screening.StartDate < nextDate))
                    .Select(sc => sc.Id))
                .List();
        }
    }
}
