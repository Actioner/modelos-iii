using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Item;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Configuration;

namespace BE.ModelosIII.Mvc.Models.Scenario
{
    public class ScenarioModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float BinSize { get; set; }
        public ConfigurationCommand Configuration { get; set; }
        public IList<ItemModel> Items { get; set; }

        public ScenarioModel()
        {
            Items = new List<ItemModel>();
            Configuration = new ConfigurationCommand();
        }
    }
}