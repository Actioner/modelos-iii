using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Scenario;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Scenario
{
    public class ItemValidator : AbstractValidator<ItemCommand>
    {
        public ItemValidator()
        {
            RuleFor(x => x.Label)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Label);

            RuleFor(x => x.Quantity)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(default(int))
                .WithLocalizedName(() => PropertyNames.Quantity);

            RuleFor(x => x.Size)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(default(int))
                .WithLocalizedName(() => PropertyNames.Size);
        }
    }
}
