using System;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Promotion;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Promotion
{
    public abstract class CreateOrEditPromotionValidator<T> : AbstractValidator<T>
        where T : PromotionCommand
    {
        protected CreateOrEditPromotionValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Name);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Description);

            RuleFor(x => x.StartDate)
              .Cascade(CascadeMode.StopOnFirstFailure)
              .NotNull()
              .NotEmpty()
              .WithLocalizedName(() => PropertyNames.StartDate);

            RuleFor(x => x.EndDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithLocalizedName(() => PropertyNames.EndDate);

            RuleFor(x => x.Days)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Days);

            RuleFor(x => x.Value)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .NotEmpty()
               .WithLocalizedName(() => PropertyNames.Value);

            When(x => x.Type == PromotionType.Percent, () =>
                RuleFor(v => v.Value)
                    .Must(BeValidPercent)
                    .WithLocalizedName(() => PropertyNames.Value));
            When(x => x.Type == PromotionType.FixedPrice, () =>
                RuleFor(v => v.Value)
                    .Must(BeValidPrice)
                    .WithLocalizedName(() => PropertyNames.Value));
            When(x => x.Type == PromotionType.NxM, () =>
                RuleFor(v => v.Value)
                    .Must(BeValidNForM)
                    .WithLocalizedName(() => PropertyNames.Value));
        }

        private bool BeValidPercent(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result > 0 && result <= 100;
            }
            return false;
        }

        private bool BeValidPrice(string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result > 0;
            }
            return false;
        }

        private bool BeValidNForM(string value)
        {
            var nm = value.Split(new[] {'x', 'X'}, StringSplitOptions.RemoveEmptyEntries);
            if (nm.Length != 2)
            {
                return false;
            }

            int n;
            if (!int.TryParse(nm[0].Trim(), out n))
            {
                return false;
            }

            int m;
            if (!int.TryParse(nm[1].Trim(), out m))
            {
                return false;
            }
            if (n <= m)
            {
                return false;
            }
            return true;
        }
    }
}
