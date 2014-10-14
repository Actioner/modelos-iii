using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Multiplex.Screen
{
    public abstract class CreateOrEditScreenValidator<T> : AbstractValidator<T>
        where T : ScreenCommand
    {
        protected CreateOrEditScreenValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Must((x, y) => BeUnique(x))
                .WithLocalizedName(() => PropertyNames.Name)
                .WithLocalizedMessage(() => ValidationMessages.AlreadyExists);

            RuleFor(x => x.Capacity)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Capacity);
            
            RuleFor(x => x.Rows)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.ScreenLayout);
        }

        public abstract bool BeUnique(T command);
    }
}
