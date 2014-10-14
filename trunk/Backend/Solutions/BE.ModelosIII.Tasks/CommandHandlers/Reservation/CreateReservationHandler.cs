using System;
using System.Linq;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.ApplicationServices;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Tasks.Commands.Reservation;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Reservation
{
    public class CreateReservationHandler : ICommandHandler<CreateReservationCommand>
    {
        private readonly IRepository<Seat> _seatRepository;
        private readonly IScreeningRepository _screeningRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IEmailService _emailService;

        public CreateReservationHandler(
            IScreeningRepository screeningRepository,
            IRepository<Seat> seatRepository,
            IPromotionRepository promotionRepository,
            IPriceRepository priceRepository,
            IReservationRepository reservationRepository,
            IEncryptionService encryptionService,
            IEmailService emailService)
        {
            this._screeningRepository = screeningRepository;
            this._seatRepository = seatRepository;
            this._promotionRepository = promotionRepository;
            this._priceRepository = priceRepository;
            this._reservationRepository = reservationRepository;
            this._encryptionService = encryptionService;
            this._emailService = emailService;
        }

        public virtual void Handle(CreateReservationCommand command)
        {
            using (var tx = _reservationRepository.DbContext.BeginTransaction())
            {
                var seats = command.SeatIds.Select(seatId => _seatRepository.Get(seatId)).ToList();
                var screening = _screeningRepository.Get(command.ScreeningId);
                var promotion = command.PromotionId.HasValue ? _promotionRepository.Get(command.PromotionId.Value) : null;
                var generalPrice = _priceRepository.GetGeneralPrice();
                int seatsQuantity = seats.Count;

                var reservation = new Domain.Reservation
                {
                    ReservationPaymentStatus = ReservationPaymentStatus.NotPaid,
                    ReservationStatus = ReservationStatus.Active,
                    Time = DateTime.Now,
                    User = command.User,
                    Seats = seats,
                    Screening = screening,
                    Promotion = promotion != null ? promotion.Name : null,
                    Total = promotion != null ? promotion.CalculateTotal(generalPrice.Value, seatsQuantity) : generalPrice.Value * seatsQuantity
                };
                reservation.Code = _encryptionService.EncryptPassword((reservation.Screening.Id + reservation.Time.ToString("yyyyMMddHHmmss") + reservation.User.Id))
                    .Substring(5, 10)
                    .ToUpper();


                _reservationRepository.SaveOrUpdate(reservation);
                _reservationRepository.DbContext.CommitTransaction();
                command.Id = reservation.Id;
            }
        }
    }
}
