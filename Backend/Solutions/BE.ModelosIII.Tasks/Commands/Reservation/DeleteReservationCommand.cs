using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Reservation
{
    public class DeleteReservationCommand : CommandBase
    {
        public int Id { get; set; }

        public Domain.User User { get; set; }
    }
}