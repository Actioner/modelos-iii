using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Scenario;

namespace BE.ModelosIII.Mvc.Models.Run
{
    public class RunListModel
    {
        public RunSearchModel RunSearch { get; set; }
        public IEnumerable<RunListItemModel> Runs { get; set; }

        public RunListModel()
        {
            RunSearch = new RunSearchModel();
            Runs = new List<RunListItemModel>();
        }
    }
}