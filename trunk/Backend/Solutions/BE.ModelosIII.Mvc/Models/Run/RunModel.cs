using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Generation;
using BE.ModelosIII.Mvc.Models.Scenario;

namespace BE.ModelosIII.Mvc.Models.Run
{
    public class RunModel
    {
        public int Id { get; set; }
        public ScenarioModel Scenario { get; set; }
        public IList<GenerationModel> Generations { get; set; }
        public System.DateTime RunOn { get; set; }

        public RunModel()
        {
            Generations = new List<GenerationModel>();
        }
    }
}