using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class PopulationOverride : IAutoMappingOverride<Population>
    {
        public void Override(AutoMapping<Population> mapping)
        {
            mapping.References(x => x.Generation)
               .Cascade
               .None();

            mapping.HasMany(x => x.Bins)
              .Cascade
              .AllDeleteOrphan()
              .Inverse();
        }
    }
}
