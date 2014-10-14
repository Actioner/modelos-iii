using System;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Reservation;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Reservation
{
    public class DeleteReservationValidator : AbstractValidator<DeleteReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationValidator(
            IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .Must((x, y) => BeValidReservation(y))
                .WithLocalizedMessage(() => ValidationMessages.ReservationCantBeCancel)
                .WithLocalizedName(() => PropertyNames.Reservation);
        }

        private bool BeValidReservation(int reservationId)
        {
            var reservation = _reservationRepository.Get(reservationId);
            if (reservation == null)
            {
                return false;
            }

            return reservation.ReservationPaymentStatus == Domain.ReservationPaymentStatus.NotPaid && reservation.ReservationStatus == Domain.ReservationStatus.Active;
        }
    }
}
