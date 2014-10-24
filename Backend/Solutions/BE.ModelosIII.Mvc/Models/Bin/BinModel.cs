using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.BinItem;
using BE.ModelosIII.Mvc.Models.Population;

namespace BE.ModelosIII.Mvc.Models.Bin
{
    public class BinModel
    {
        public int Id { get; set; }
        public PopulationModel Population { get; set; }
        public IList<BinItemModel> BinItems { get; set; }
        public float Filled { get; set; }
        public float Capacity { get; set; }

        public BinModel()
        {
            BinItems = new List<BinItemModel>();
        }
    }
}
