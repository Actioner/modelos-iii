using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Gateway
{
    public class ProcessPaymentCommand : CommandBase
    {
        public int ReservationId { get; set; }
        public string CardNumber { get; set; }
        public string CardOwner { get; set; }
        public string VerificationCode { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
    }
}
