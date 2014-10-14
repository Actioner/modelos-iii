using System.Collections.Generic;
using System.Linq;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Seat : Entity
    {
        public virtual int Number { get; set; }
        public virtual Row Row { get; set; }
        public virtual IList<Reservation> Reservations { get; set; }

        public virtual bool AvailableForScreening(Screening screening)
        {
            return !Reservations.Any() || Reservations.Where(r => r.Screening.Id == screening.Id).All(r => r.ReservationStatus == ReservationStatus.Cancel);
        }

        public virtual bool ReservedForScreening(Screening screening)
        {
            return Reservations.Where(r => r.Screening.Id == screening.Id).Any(r => r.ReservationStatus != ReservationStatus.Cancel && r.ReservationPaymentStatus == ReservationPaymentStatus.NotPaid);
        }


        public virtual bool PurchasedForScreening(Screening screening)
        {
            return Reservations.Where(r => r.Screening.Id == screening.Id).Any(r => r.ReservationStatus != ReservationStatus.Cancel && r.ReservationPaymentStatus == ReservationPaymentStatus.Paid);
        }

        public override string ToString()
        {
            return Row.Name + Number;
        }

    }
}