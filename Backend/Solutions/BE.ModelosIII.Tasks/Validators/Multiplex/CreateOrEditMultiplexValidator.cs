using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Multiplex
{
    public abstract class CreateOrEditMultiplexValidator<T> : AbstractValidator<T>
        where T : MultiplexCommand
    {
        protected CreateOrEditMultiplexValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Must((x, y) => BeUnique(x))
                .WithLocalizedName(() => PropertyNames.Name)
                .WithLocalizedMessage(() => ValidationMessages.AlreadyExists);

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Address);

            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.City);

            RuleFor(x => x.Latitude)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Latitude);

            RuleFor(x => x.Longitude)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Longitude);
        }

        public abstract bool BeUnique(T command);

    }
}
