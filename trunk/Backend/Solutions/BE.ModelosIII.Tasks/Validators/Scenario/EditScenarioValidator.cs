using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Scenario;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Scenario
{
    public class EditScenarioValidator : CreateOrEditScenarioValidator<EditScenarioCommand>
    {
        private readonly IScenarioRepository _scenarioRepository;

        public EditScenarioValidator(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;

            RuleFor(x => x.Id)
             .GreaterThan(default(int));
        }

        public override bool BeUnique(EditScenarioCommand command)
        {
            var scenario = _scenarioRepository.GetByName(command.Name);
            return scenario == null || scenario.Id == command.Id;
        }
    }
}
