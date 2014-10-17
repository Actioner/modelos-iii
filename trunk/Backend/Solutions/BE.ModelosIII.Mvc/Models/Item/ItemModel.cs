using System.Collections.Generic;

namespace BE.ModelosIII.Mvc.Models.Item
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int Quantity { get; set; }
        public float Size { get; set; }
    }
}