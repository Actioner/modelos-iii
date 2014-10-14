using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Multiplex.Screen
{
    public class CreateScreenValidator : CreateOrEditScreenValidator<CreateScreenCommand>
    {
        private readonly IScreenRepository _screenRepository;
        private readonly IMultiplexRepository _multiplexRepository;

        public CreateScreenValidator(IScreenRepository screenRepository,
            IMultiplexRepository multiplexRepository)
        {
            this._screenRepository = screenRepository;
            this._multiplexRepository = multiplexRepository;
         
            RuleFor(x => x.Id)
                .Equal(default(int));
        }

        public override bool BeUnique(CreateScreenCommand command)
        {
            var multiplex = _multiplexRepository.Get(command.MultiplexId);
            return _screenRepository.GetByMultiplexAndName(multiplex, command.Name) == null;
        }
    }
}