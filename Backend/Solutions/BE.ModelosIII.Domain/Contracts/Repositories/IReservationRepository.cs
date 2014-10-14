using System.Collections.Generic;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Domain.Contracts.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IList<Reservation> GetByScreening(Screening screening);
        IList<Reservation> GetByUser(User user);
    }
}