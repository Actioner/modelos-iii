using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Multiplex
{
    public class CreateMultiplexValidator : CreateOrEditMultiplexValidator<CreateMultiplexCommand>
    {
        private readonly IMultiplexRepository _multiplexRepository;

        public CreateMultiplexValidator(IMultiplexRepository multiplexRepository)
        {
            _multiplexRepository = multiplexRepository;

            RuleFor(x => x.Id)
                .Equal(default(int));
        }

        public override bool BeUnique(CreateMultiplexCommand command)
        {
            return _multiplexRepository.GetByName(command.Name) == null;
        }
    }
}