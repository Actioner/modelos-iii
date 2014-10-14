using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class SeatOverride : IAutoMappingOverride<Seat>
    {
        public void Override(AutoMapping<Seat> mapping)
        {
            mapping.References(x => x.Row)
                .Cascade.None();
        }
    }
}
