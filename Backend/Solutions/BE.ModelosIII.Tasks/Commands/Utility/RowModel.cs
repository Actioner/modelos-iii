using System.Collections.Generic;

namespace BE.ModelosIII.Tasks.Commands.Utility
{
    public class RowModel
    {
        public int Id { get; set; }
        public int ScreenId { get; set; }
        public string Name { get; set; }
        public IList<SeatModel> Seats { get; set; }

        public RowModel()
        {
            Seats = new List<SeatModel>();
        }
    }
}
