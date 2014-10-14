using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using SharpArch.Domain;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Infrastructure.Repositories
{
    public class Reservationepository : NHibernateRepository<Reservation>, IReservationRepository
    {
        public IList<Reservation> GetByScreening(Screening screening)
        {
            Check.Require(screening != null);

            return Session.QueryOver<Reservation>()
                .Where(r => r.Screening.Id == screening.Id)
                .List();
        }

        public IList<Reservation> GetByUser(User user)
        {
            Check.Require(user != null);

            return Session.QueryOver<Reservation>()
                .Where(r => r.User.Id == user.Id && r.ReservationStatus == ReservationStatus.Active)
                .List();
        }
    }
}
