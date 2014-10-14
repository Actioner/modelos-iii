using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class ScreenOverride : IAutoMappingOverride<Screen>
    {
        public void Override(AutoMapping<Screen> mapping)
        {
            mapping.HasMany(x => x.Rows)
                .Inverse()
                .Cascade.AllDeleteOrphan();


            mapping.HasMany(x => x.Screenings)
                .Cascade
                .AllDeleteOrphan();
        }
    }
}
