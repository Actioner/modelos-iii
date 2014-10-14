using BE.ModelosIII.Domain;
using BE.ModelosIII.Mvc.Models.Report;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public class AvailabilityReportQuery : NHibernateQuery, IAvailabilityReportQuery
    {
        public AvailabilityReportModel GetReport(Screening screening)
        {
            Reservation reservation = null;

            var reservedSeatCount = Session.QueryOver<Reservation>(() => reservation)
                .Left.JoinQueryOver(s => s.Seats)
                .Where(s => reservation.Screening.Id == screening.Id
                    && reservation.ReservationStatus != ReservationStatus.Cancel
                    && reservation.ReservationPaymentStatus == ReservationPaymentStatus.NotPaid)
                .ToRowCountQuery()
                .FutureValue<int>(); 
            
            var purchasedSeatCount = Session.QueryOver<Reservation>(() => reservation)
                .Left.JoinQueryOver(s => s.Seats)
                .Where(s => reservation.Screening.Id == screening.Id
                    && reservation.ReservationStatus != ReservationStatus.Cancel
                    && reservation.ReservationPaymentStatus == ReservationPaymentStatus.Paid)
                .ToRowCountQuery()
                .FutureValue<int>();

            var totalSeatCount = Session.QueryOver<Seat>()
                .Left.JoinQueryOver(s => s.Row)
                .Where(r => r.Screen.Id == screening.Screen.Id)
                .ToRowCountQuery()
                .FutureValue<int>();

            return new AvailabilityReportModel
                       {
                           FreeCount = totalSeatCount.Value - reservedSeatCount.Value - purchasedSeatCount.Value,
                           PurchasedCount = purchasedSeatCount.Value,
                           ReservedCount =  reservedSeatCount.Value
                       };
        }
    }
}