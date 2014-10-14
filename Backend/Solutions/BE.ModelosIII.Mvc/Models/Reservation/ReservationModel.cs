namespace BE.ModelosIII.Mvc.Models.Reservation
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Seats { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ScreeningDate { get; set; }
        public string ScreeningTime { get; set; }
        public string ScreeningEndTime { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string Movie { get; set; }
        public string MoviePoster { get; set; }
        public string Promotion { get; set; }
        public string Multiplex { get; set; }
        public string Type { get; set; }
        public double Total { get; set; }
    }
}