using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class RunOverride : IAutoMappingOverride<Run>
    {
        public void Override(AutoMapping<Run> mapping)
        {
            mapping.References(x => x.Scenario)
               .Cascade
               .None();

            mapping.HasMany(x => x.Generations)
              .Cascade
              .AllDeleteOrphan()
              .Inverse();
        }
    }
}
