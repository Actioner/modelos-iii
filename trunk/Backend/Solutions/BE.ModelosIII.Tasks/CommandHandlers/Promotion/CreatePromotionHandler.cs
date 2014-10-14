using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Promotion;

namespace BE.ModelosIII.Tasks.CommandHandlers.Promotion
{
    public class CreatePromotionHandler :PromotionHandler<CreatePromotionCommand>
    {
        public CreatePromotionHandler(
                IPromotionRepository promotionRepository,
                IMappingEngine mappingEngine)
            : base(promotionRepository, mappingEngine)
        {
        }

        protected override Domain.Promotion MapCommandToPromotion(CreatePromotionCommand command)
        {
            return MappingEngine.Map<Domain.Promotion>(command);
        }
    }
}
