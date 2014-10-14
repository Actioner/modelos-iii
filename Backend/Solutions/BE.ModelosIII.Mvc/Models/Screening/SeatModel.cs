namespace BE.ModelosIII.Mvc.Models.Screening
{
    public class SeatModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Available { get; set; }
        public bool Reserved { get; set; }
        public bool Purchased { get; set; }
    }
}