using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Promotion;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Promotion
{
    public class GeneralPriceHandler : ICommandHandler<GeneralPriceCommand>
    {
        private readonly IPriceRepository _priceRepository;

        public GeneralPriceHandler(
            IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public void Handle(GeneralPriceCommand command)
        {
            var price = _priceRepository.GetGeneralPrice();

            price.Value = command.Value;

            _priceRepository.SaveOrUpdate(price);
        }
    }
}
