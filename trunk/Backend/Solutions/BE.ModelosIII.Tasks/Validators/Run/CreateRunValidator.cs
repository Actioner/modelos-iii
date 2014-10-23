using BE.ModelosIII.Tasks.Commands.Run;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Run
{
    public class CreateRunValidator : CreateOrEditRunValidator<CreateRunCommand>
    {
        public CreateRunValidator()
        {
            RuleFor(x => x.Id)
                .Equal(default(int));
        }
    }
}