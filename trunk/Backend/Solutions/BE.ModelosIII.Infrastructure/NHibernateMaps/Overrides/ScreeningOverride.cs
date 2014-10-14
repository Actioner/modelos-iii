using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class ScreeningOverride : IAutoMappingOverride<Screening>
    {
        public void Override(AutoMapping<Screening> mapping)
        {
            mapping.IgnoreProperty(x => x.EndDate);
        }
    }
}
