using System;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Gateway;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Gateway
{
    public class ProcessPaymentHandler : ICommandHandler<ProcessPaymentCommand, CommandResult<ProcessPaymentCommand>>
    {
        private readonly IReservationRepository _reservationRepository;

        public ProcessPaymentHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public CommandResult<ProcessPaymentCommand> Handle(ProcessPaymentCommand command)
        {
            if (command.CardNumber == "123412341234")
            {
                using (var tx = _reservationRepository.DbContext.BeginTransaction())
                {
                    var reservation = _reservationRepository.Get(command.ReservationId);
                    reservation.ReservationPaymentStatus = ReservationPaymentStatus.Paid;
                    _reservationRepository.SaveOrUpdate(reservation);

                    _reservationRepository.DbContext.CommitTransaction();
                }

                return new CommandResult<ProcessPaymentCommand>
                {
                    Success = true,
                    Message = Resources.PaymentMessages.PaymentSuccess
                };
            }

            if (command.CardNumber == "432143214321")
            {
                return new CommandResult<ProcessPaymentCommand>
                {
                    Success = false,
                    Message = Resources.PaymentMessages.InsuficientFunds
                };
            } 

            if (command.CardNumber == "123443211234321")
            {
                return new CommandResult<ProcessPaymentCommand>
                {
                    Success = false,
                    Message = Resources.PaymentMessages.CancelCard
                };
            }

            return new CommandResult<ProcessPaymentCommand>
            {
                Success = false,
                Message = Resources.PaymentMessages.InvalidCardNumber
            };


        }
    }
}
