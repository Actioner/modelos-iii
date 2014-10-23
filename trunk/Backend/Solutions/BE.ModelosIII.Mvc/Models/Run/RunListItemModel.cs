using BE.ModelosIII.Mvc.Models.Scenario;

namespace BE.ModelosIII.Mvc.Models.Run
{
    public class RunListItemModel
    {
        public int Id { get; set; }
        public ScenarioModel Scenario { get; set; }
        public System.DateTime RunOn { get; set; }
    }
}