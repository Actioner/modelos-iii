using System.Collections.Generic;

namespace BE.ModelosIII.Mvc.Models.Reservation
{
    public class ReservationListModel
    {
        public ReservationSearchModel ReservationSearch { get; set; }
        public IList<ReservationModel> Reservations { get; set; }

        public ReservationListModel()
        {
            ReservationSearch = new ReservationSearchModel();
            Reservations = new List<ReservationModel>();
        }
    }
}