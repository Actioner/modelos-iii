using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Population;
using BE.ModelosIII.Mvc.Models.Run;

namespace BE.ModelosIII.Mvc.Models.Generation
{
    public class GenerationModel
    {
        public int Id { get; set; }
        public RunModel Run { get; set; }
        public IList<PopulationModel> Populations { get; set; }
        public int Number { get; set; }

        public GenerationModel()
        {
            Populations = new List<PopulationModel>();
        }
    }
}