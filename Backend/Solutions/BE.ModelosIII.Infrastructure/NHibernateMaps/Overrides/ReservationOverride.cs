using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class ReservationOverride : IAutoMappingOverride<Reservation>
    {
        public void Override(AutoMapping<Reservation> mapping)
        {
            //mapping.HasManyToMany(x => x.Seats)
            //    .Cascade
            //    .All();
        }
    }
}
