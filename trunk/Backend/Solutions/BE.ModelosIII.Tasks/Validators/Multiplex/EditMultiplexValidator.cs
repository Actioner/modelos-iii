using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Multiplex
{
    public class EditMultiplexValidator : CreateOrEditMultiplexValidator<EditMultiplexCommand>
    {
        private readonly IMultiplexRepository _multiplexRepository;

         public EditMultiplexValidator(IMultiplexRepository multiplexRepository)
        {
            _multiplexRepository = multiplexRepository;

            RuleFor(x => x.Id)
             .GreaterThan(default(int));
        }


         public override bool BeUnique(EditMultiplexCommand command)
         {
             var screen = _multiplexRepository.GetByName(command.Name);
             return screen == null || screen.Id == command.Id;
         }
    }
}
