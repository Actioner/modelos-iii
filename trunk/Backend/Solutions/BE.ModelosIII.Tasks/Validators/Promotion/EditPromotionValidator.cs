using BE.ModelosIII.Tasks.Commands.Promotion;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Promotion
{
    public class EditPromotionValidator : CreateOrEditPromotionValidator<EditPromotionCommand>
    {
        public EditPromotionValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(default(int));
        }
    }
}
