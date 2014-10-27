using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Scenario
{
    public class CreateRunHandler : ScenarioHandler<CreateScenarioCommand>
    {
        public CreateRunHandler(
                IRepository<Domain.Scenario> scenarioRepository,
                IMappingEngine mappingEngine)
            : base(scenarioRepository, mappingEngine)
        {
        }

        protected override Domain.Scenario MapCommandToScenario(CreateScenarioCommand command)
        {
            var scenario = MappingEngine.Map<Domain.Scenario>(command);
            foreach (var item in scenario.Items)
            {
                item.Scenario = scenario;
            }
            return scenario;
        }
    }
}
