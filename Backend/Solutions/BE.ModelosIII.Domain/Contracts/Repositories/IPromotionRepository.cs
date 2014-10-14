using System;
using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IList<Movie> GetByTitle(string title);
        IList<Movie> GetByOriginalTitle(string title);

        IList<Movie> FindAllWithScreening(DateTime dateFrom, DateTime dateTo);
        IList<Movie> FindAllWithScreeningAt(Multiplex multiplex, DateTime dateFrom, DateTime dateTo);
    }
}