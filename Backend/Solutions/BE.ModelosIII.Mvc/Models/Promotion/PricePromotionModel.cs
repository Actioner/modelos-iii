using System.Collections.Generic;
using BE.ModelosIII.Tasks.Commands.Promotion;

namespace BE.ModelosIII.Mvc.Models.Promotion
{
    public class PricePromotiontModel
    {
        public GeneralPriceCommand GeneralPrice { get; set; }
        public IEnumerable<PromotionModel> Promotions { get; set; }

        public PricePromotiontModel()
        {
            Promotions = new List<PromotionModel>();
        }
    }
}