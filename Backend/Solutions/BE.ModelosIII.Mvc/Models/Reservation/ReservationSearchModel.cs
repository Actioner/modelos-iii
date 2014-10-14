using System;

namespace BE.ModelosIII.Mvc.Models.Reservation
{
    public class ReservationSearchModel
    {
        public int MultiplexId { get; set; }
        public int ScreenId { get; set; }
        public int ScreeningId { get; set; }
        public DateTime? Date { get; set; }
    }
}