using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Scenario
{
    public class DeleteScenarioCommand : CommandBase
    {
        public int Id { get; set; }
    }
}
