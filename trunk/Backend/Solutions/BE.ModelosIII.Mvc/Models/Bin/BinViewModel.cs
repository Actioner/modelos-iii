using System.Linq;
using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.BinItem;
using BE.ModelosIII.Mvc.Models.Population;

namespace BE.ModelosIII.Mvc.Models.Bin
{
    public class BinViewModel
    {
        public int Id { get; set; }
        public IList<BinItemViewModel> BinItems { get; set; }
        public float Filled { get; set; }
        public float Capacity { get; set; }

        public IList<string> Items 
        {
            get 
            {
                return BinItems.GroupBy(bi => bi.Label).Select(bg => bg.Count() + "x " + bg.Key).ToList();
            }
        }

        public BinViewModel()
        {
            BinItems = new List<BinItemViewModel>();
        }
    }
}
