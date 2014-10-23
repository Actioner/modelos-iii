using BE.ModelosIII.Mvc.Models.Bin;

namespace BE.ModelosIII.Mvc.Models.BinItem
{
    public class BinItemModel
    {
        public int Id { get; set; }
        public BinModel Bin { get; set; }
        public string Label { get; set; }
        public float Size { get; set; }
    }
}
