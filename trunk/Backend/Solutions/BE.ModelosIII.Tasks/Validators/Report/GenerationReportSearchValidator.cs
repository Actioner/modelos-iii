using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Scenario;
using FluentValidation;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Tasks.Validators.Report
{
    public class GenerationReportSearchValidator : AbstractValidator<GenerationReportSearchCommand>
    {
        public GenerationReportSearchValidator()
        {
            RuleFor(x => x.ScenarioId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithLocalizedName(() => PropertyNames.Scenario);


            RuleFor(x => x.RunId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithLocalizedName(() => PropertyNames.Run);
        }
    }
}
