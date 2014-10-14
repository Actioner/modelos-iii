using System.Collections.Generic;

namespace BE.ModelosIII.Mvc.Models.Screening
{
    public class RowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<SeatModel> Seats { get; set; }

        public RowModel()
        {
            Seats = new List<SeatModel>();
        }
    }
}