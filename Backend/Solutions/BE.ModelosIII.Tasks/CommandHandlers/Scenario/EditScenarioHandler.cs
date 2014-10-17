using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Scenario
{
    public class EditScenarioHandler : ScenarioHandler<EditScenarioCommand>
    {
        public EditScenarioHandler(
                IRepository<Domain.Scenario> scenarioRepository,
                IMappingEngine mappingEngine)
            : base(scenarioRepository, mappingEngine)
        {
        }

        protected override Domain.Scenario MapCommandToScenario(EditScenarioCommand command)
        {
            return MappingEngine.Map(command, ScenarioRepository.Get(command.Id));
        }
    }
}