﻿using System;
using System.Net;
using System.Text.RegularExpressions;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Scenario;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Scenario
{
    public abstract class CreateOrEditScenarioValidator<T> : AbstractValidator<T>
        where T : ScenarioCommand
    {
        protected CreateOrEditScenarioValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Must((x, y) => BeUnique(x))
                .WithLocalizedName(() => PropertyNames.Name)
                .WithLocalizedMessage(() => ValidationMessages.AlreadyExists); ;

            RuleFor(x => x.BinSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(default(int))
                .WithLocalizedName(() => PropertyNames.BinSize);

            RuleFor(x => x.Items)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .SetCollectionValidator(new ItemValidator())
                .WithLocalizedName(() => PropertyNames.Items);

            RuleFor(x => x.Configuration)
                .SetValidator(new ConfigurationValidator());
        }

        public abstract bool BeUnique(T command);
    }
}
