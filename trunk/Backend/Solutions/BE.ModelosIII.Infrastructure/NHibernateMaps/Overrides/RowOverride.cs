using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class RowOverride : IAutoMappingOverride<Row>
    {
        public void Override(AutoMapping<Row> mapping)
        {
            mapping.References(x => x.Screen)
                .Cascade
                .None();

            mapping.HasMany(x => x.Seats)
                .Cascade
                .AllDeleteOrphan();
        }
    }
}
