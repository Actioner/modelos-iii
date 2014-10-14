using System;
using System.Linq;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.Helpers.CustomFluentAdapters;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Reservation;
using FluentValidation;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.Validators.Reservation
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IPromotionRepository _promotionRepository;
        private Domain.Screening _screening;

        public CreateReservationValidator(
            IScreeningRepository screeningRepository,
            IPromotionRepository promotionRepository,
            IRepository<Seat> seatRepository)
        {
            _screeningRepository = screeningRepository;
            _promotionRepository = promotionRepository;

            RuleFor(x => x.ScreeningId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .Must((x, y) => BeValidScreening(y))
                .WithLocalizedMessage(() => ValidationMessages.ScreeningInThePast)
                .WithLocalizedName(() => PropertyNames.Screening);

            RuleFor(x => x.SeatIds)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .SetCollectionValidator(x => new SeatValidator(x, seatRepository, screeningRepository))
                .WithLocalizedName(() => PropertyNames.Seats);

            When(x => x.PromotionId.HasValue, () =>
                RuleFor(p => p.PromotionId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .Must((x, y) => BeValidPromotion(y.Value))
                .WithLocalizedMessage(() => ValidationMessages.PromotionNotAvailable)
                .WithLocalizedName(() => PropertyNames.Promotion));
        }

        private bool BeValidPromotion(int id)
        {
            return _promotionRepository.GetAvailable(_screening.StartDate.Date).Any(p => p.Id == id);
        }

        private bool BeValidScreening(int screeningId)
        {
            _screening = _screeningRepository.Get(screeningId);
            if (_screening == null)
            {
                return false;
            }

            return _screening.StartDate > DateTime.Now;
        }

        private class SeatValidator : AbstractValidator<int>
        {
            private readonly IRepository<Seat> _seatRepository;
            private readonly IScreeningRepository _screeningRepository;

            public SeatValidator(CreateReservationCommand command, IRepository<Seat> seatRepository, IScreeningRepository screeningRepository)
            {
                this._seatRepository = seatRepository;
                this._screeningRepository = screeningRepository;

                RuleFor(x => x)
                    .NotEmpty()
                    .GreaterThan(0)
                    .Must((x, y) => BeValidSeat(command, y))
                    .WithLocalizedName(() => PropertyNames.Seat)
                    .WithLocalizedMessage(() => ValidationMessages.AlreadyHasReservation);
            }

            private bool BeValidSeat(CreateReservationCommand command, int seatId)
            {
                var seat = _seatRepository.Get(seatId);
                var screening = _screeningRepository.Get(command.ScreeningId);

                return seat != null && seat.AvailableForScreening(screening);
            }
        }
    }
}
