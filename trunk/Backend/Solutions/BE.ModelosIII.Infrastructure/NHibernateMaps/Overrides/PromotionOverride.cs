using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class PromotionOverride : IAutoMappingOverride<Promotion>
    {
        public void Override(AutoMapping<Promotion> mapping)
        {
            mapping.HasMany(x => x.Days)
                .Table("PromotionDays")
                .Element("DayOfWeek");
        }
    }
}
