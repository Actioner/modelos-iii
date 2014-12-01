using BE.ModelosIII.Tasks.Commands.Configuration;
namespace BE.ModelosIII.Tasks.Commands.Scenario
{
    public class CreateScenarioCommand : ScenarioCommand
    {
        public CreateScenarioCommand()
        {
            //Default Values
            this.Configuration = new ConfigurationCommand
            {
                CrossoverProbability = 100,
                MutationProbability = 66,
                PopulationSize = 100,
                StopCriterion = Domain.Enums.StopCriterion.StatsChange,
                StopDepth = 2,
                Report = false
            };
        }
    }
}
