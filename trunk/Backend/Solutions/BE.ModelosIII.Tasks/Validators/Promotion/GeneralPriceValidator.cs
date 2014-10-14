using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Promotion;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Promotion
{
    public class GeneralPriceValidator : AbstractValidator<GeneralPriceCommand>
    {

        public GeneralPriceValidator()
        {
            RuleFor(x => x.Value)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.GeneralPrice);
        }
    }
}
