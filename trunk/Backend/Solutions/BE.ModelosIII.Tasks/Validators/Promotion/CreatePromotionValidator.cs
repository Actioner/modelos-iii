using BE.ModelosIII.Tasks.Commands.Promotion;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Promotion
{
    public class CreatePromotionValidator : CreateOrEditPromotionValidator<CreatePromotionCommand>
    {

        public CreatePromotionValidator()
        {

            RuleFor(x => x.Id)
                .Equal(default(int));
        }
    }
}