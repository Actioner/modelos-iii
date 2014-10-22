using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.ModelosIII.Mvc.Models.Item;
using BE.ModelosIII.Resources;
using FluentValidation;

namespace BE.ModelosIII.Mvc.Validators.Scenario
{
    public class ItemValidator : AbstractValidator<ItemModel>
    {
        public ItemValidator()
        {
            RuleFor(x => x.Label)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Label);

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Quantity);
                
            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Size);
        }
    }
}