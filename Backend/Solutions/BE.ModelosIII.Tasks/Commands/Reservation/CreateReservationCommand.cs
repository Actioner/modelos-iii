using System.Collections.Generic;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Reservation
{
    public class CreateReservationCommand : CommandBase
    {
        public int Id { get; set; }
        public int ScreeningId { get; set; }
        public int? PromotionId { get; set; }
        public IList<int> SeatIds { get; set; }

        public Domain.User User { get; set; }
    }
}