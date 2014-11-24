using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Scenario;
using FluentValidation;
using BE.ModelosIII.Tasks.Commands.Configuration;

namespace BE.ModelosIII.Tasks.Validators.Scenario
{
    public class ConfigurationValidator : AbstractValidator<ConfigurationCommand>
    {
        public ConfigurationValidator()
        {
            RuleFor(x => x.CrossoverProbability)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty()
                    .InclusiveBetween(0, 100)
                    .WithLocalizedName(() => PropertyNames.CrossoverProbability);

            RuleFor(x => x.MutationProbability)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(0, 100)
                .WithLocalizedName(() => PropertyNames.MutationProbability);

            RuleFor(x => x.PopulationSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.PopulationSize);

            RuleFor(x => x.StopDepth)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.StopDepth);
        }
    }
}
