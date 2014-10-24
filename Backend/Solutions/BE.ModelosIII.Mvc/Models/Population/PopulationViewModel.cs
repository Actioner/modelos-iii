using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Bin;
using BE.ModelosIII.Mvc.Models.Generation;

namespace BE.ModelosIII.Mvc.Models.Population
{
    public class PopulationViewModel
    {
        public int Id { get; set; }
        public IList<BinViewModel> Bins { get; set; }
        public int Number { get; set; }
        public float Fitness { get; set; }
        public int BinCount { get; set; }

        public PopulationViewModel()
        {
            Bins = new List<BinViewModel>();
        }
    }
}
