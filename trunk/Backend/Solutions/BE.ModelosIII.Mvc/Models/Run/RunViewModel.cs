using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Generation;
using BE.ModelosIII.Mvc.Models.Item;
using BE.ModelosIII.Mvc.Models.Population;
using BE.ModelosIII.Mvc.Models.Scenario;

namespace BE.ModelosIII.Mvc.Models.Run
{
    public class RunViewModel
    {
        public int Id { get; set; }
        public string ScenarioName { get; set; }
        public float BinSize { get; set; }
        public IList<ItemModel> Items { get; set; }
        public System.DateTime RunOn { get; set; }
        public PopulationViewModel Population { get; set; }

        public RunViewModel()
        {
            Items = new List<ItemModel>();
        }
    }
}