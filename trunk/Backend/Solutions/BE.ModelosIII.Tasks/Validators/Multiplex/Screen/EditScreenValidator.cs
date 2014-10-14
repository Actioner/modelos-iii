using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Multiplex.Screen
{
    public class EditScreenValidator : CreateOrEditScreenValidator<EditScreenCommand>
    {
        private readonly IScreenRepository _screenRepository;
        private readonly IMultiplexRepository _multiplexRepository;

        public EditScreenValidator(IScreenRepository screenRepository,
            IMultiplexRepository multiplexRepository)
        {
            this._screenRepository = screenRepository;
            this._multiplexRepository = multiplexRepository;


            RuleFor(x => x.Id)
             .GreaterThan(default(int));
        }

        public override bool BeUnique(EditScreenCommand command)
        {
            var multiplex = _multiplexRepository.Get(command.MultiplexId);

            var screen = _screenRepository.GetByMultiplexAndName(multiplex, command.Name);
            return screen == null || screen.Id == command.Id;
        }
    }
}
