using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Promotion;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Promotion
{
    public class DeletePromotionHandler : ICommandHandler<DeletePromotionCommand>
    {
        private readonly IPromotionRepository _promotionRepository;

        public DeletePromotionHandler(
            IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public void Handle(DeletePromotionCommand command)
        {
            var promotion = _promotionRepository.Get(command.Id);
            if (promotion != null)
            {
                _promotionRepository.Delete(promotion);
            }
        }
    }
}
