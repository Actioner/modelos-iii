using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Promotion;

namespace BE.ModelosIII.Tasks.CommandHandlers.Promotion
{
    public class EditPromotionHandler : PromotionHandler<EditPromotionCommand>
    {
        public EditPromotionHandler(
                IPromotionRepository promotionRepository,
                IMappingEngine mappingEngine)
            : base(promotionRepository, mappingEngine)
        {
        }

        protected override Domain.Promotion MapCommandToPromotion(EditPromotionCommand command)
        {
            return MappingEngine.Map(command, PromotionRepository.Get(command.Id));
        }
    }
}