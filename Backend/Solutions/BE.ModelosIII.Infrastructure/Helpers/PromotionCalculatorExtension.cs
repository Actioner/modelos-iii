using System;
using BE.ModelosIII.Domain;

namespace BE.ModelosIII.Infrastructure.Helpers
{
    public static class PromotionCalculatorExtension
    {
        public static double CalculateTotal(this Promotion promotion, double generalPrice, int seatQuantity)
        {
            if (seatQuantity == 0)
            {
                return 0;
            }
            if (promotion.Type == PromotionType.FixedPrice)
            {
                return double.Parse(promotion.Value) * seatQuantity;
            }

            if (promotion.Type == PromotionType.Percent)
            {
                int percent = int.Parse(promotion.Value);
                return generalPrice * seatQuantity * (100 - percent) / 100;
            }

            if (promotion.Type == PromotionType.NxM)
            {
                var nm = promotion.Value.Split(new[] {'x', 'X'}, StringSplitOptions.RemoveEmptyEntries);
                int n = int.Parse(nm[0]);
                int m = int.Parse(nm[1]);

                int entries = (int) Math.Floor((double) (seatQuantity / n));

                return generalPrice * (entries * m + Math.Min(seatQuantity % n, m));
            }

            return 0;
        }
    }
}
