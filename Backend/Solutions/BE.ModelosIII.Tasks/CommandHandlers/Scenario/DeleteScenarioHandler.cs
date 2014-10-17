using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;

namespace BE.ModelosIII.Tasks.CommandHandlers.Scenario
{
    public class DeleteScenarioHandler : ICommandHandler<DeleteScenarioCommand>
    {
        private readonly IRepository<Domain.Scenario> _scenarioRepository;

        public DeleteScenarioHandler(
            IRepository<Domain.Scenario> scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;
        }

        public void Handle(DeleteScenarioCommand command)
        {
            var scenario = _scenarioRepository.Get(command.Id);
            if (scenario != null)
            {
                _scenarioRepository.Delete(scenario);
            }
        }
    }
}
