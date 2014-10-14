using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Report;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Report
{
    public class MostSoldValidator : AbstractValidator<MostSoldCommand>
    {
        public MostSoldValidator()
        {
            RuleFor(x => x.From)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.StartDate);

            RuleFor(x => x.To)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(x => x.From)
                .WithLocalizedName(() => PropertyNames.EndDate);
        }
    }
}