using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Scenario;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Scenario
{
    public class CreateScenarioValidator : CreateOrEditScenarioValidator<CreateScenarioCommand>
    {
        private readonly IScenarioRepository _scenarioRepository;

        public CreateScenarioValidator(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;

            RuleFor(x => x.Id)
                .Equal(default(int));
        }

        public override bool BeUnique(CreateScenarioCommand command)
        {
            return _scenarioRepository.GetByName(command.Name) == null;
        }
    }
}