using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Bin;
using BE.ModelosIII.Mvc.Models.Generation;

namespace BE.ModelosIII.Mvc.Models.Population
{
    public class PopulationModel
    {
        public int Id { get; set; }
        public GenerationModel Generation { get; set; }
        public IList<BinModel> Bins { get; set; }
        public int Number { get; set; }
        public float Fitness { get; set; }
        public int BinCount { get; set; }

        public PopulationModel()
        {
            Bins = new List<BinModel>();
        }
    }
}
