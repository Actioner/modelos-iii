using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Promotion
{
    public class GeneralPriceCommand : CommandBase
    {
        public double Value { get; set; }
    }
}
