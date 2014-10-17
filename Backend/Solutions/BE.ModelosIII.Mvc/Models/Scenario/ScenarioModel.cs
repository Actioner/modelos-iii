using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Item;

namespace BE.ModelosIII.Mvc.Models.Scenario
{
    public class ScenarioModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float BinSize { get; set; }
        public IList<ItemModel> Items { get; set; }

        public ScenarioModel()
        {
            Items = new List<ItemModel>();
        }
    }
}