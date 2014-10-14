using System;
using System.Web;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Gateway;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Gateway
{
    public class ProcessPaymentValidator : AbstractValidator<ProcessPaymentCommand>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        
        public ProcessPaymentValidator(
            IReservationRepository reservationRepository,
            IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;

            RuleFor(x => x.ReservationId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .Must((x, y) => BeValidReservation(y))
                .WithLocalizedMessage(() => ValidationMessages.InvalidReservationForGateway)
                .WithLocalizedName(() => PropertyNames.Reservation);

            RuleFor(x => x.CardNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.CardNumber);

            RuleFor(x => x.CardOwner)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.CardOwner);

            RuleFor(x => x.VerificationCode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.VerificationCode);

            RuleFor(x => x.ExpirationMonth)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(1, 12)
                .WithLocalizedName(() => PropertyNames.ExpirationMonth);

            RuleFor(x => x.ExpirationYear)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithLocalizedName(() => PropertyNames.ExpirationYear);
        }
    
        private bool BeValidReservation(int reservationId)
        {
            var reservation = _reservationRepository.Get(reservationId);
            if (reservation == null)
            {
                return false;
            }
            var currentUser = _userRepository.GetByEmail(HttpContext.Current.User.Identity.Name);
            
            return 
                reservation.User.Id == currentUser.Id
                && reservation.ReservationStatus == ReservationStatus.Active
                && reservation.ReservationPaymentStatus == ReservationPaymentStatus.NotPaid 
                && reservation.Screening.StartDate > DateTime.Now;
        }

    }
}
