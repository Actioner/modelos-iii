using System;
using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IScreeningRepository : IRepository<Screening>
    {
        IList<Screening> GetForMovie(Movie movie, DateTime start, DateTime end);
        IList<Screening> GetByMultiplexAndScreenAndDate(Multiplex multiplex, Screen screen, DateTime date);
        IList<Screening> GetByScreenAndDateRange(Screen screen, DateTime start, DateTime end);
    }
}