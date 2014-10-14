using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Reservation;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Reservation
{
    public class DeleteReservationHandler : ICommandHandler<DeleteReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationHandler(
            IReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }

        public virtual void Handle(DeleteReservationCommand command)
        {
            using (var tx = _reservationRepository.DbContext.BeginTransaction())
            {
                var reservation = _reservationRepository.Get(command.Id);
                reservation.ReservationStatus = ReservationStatus.Cancel;

                _reservationRepository.SaveOrUpdate(reservation);
                _reservationRepository.DbContext.CommitTransaction();
            }
        }
    }
}
