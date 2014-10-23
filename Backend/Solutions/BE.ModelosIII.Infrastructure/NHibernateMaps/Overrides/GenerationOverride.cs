using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class GenerationOverride : IAutoMappingOverride<Generation>
    {
        public void Override(AutoMapping<Generation> mapping)
        {
            mapping.References(x => x.Run)
               .Cascade
               .None();

            mapping.HasMany(x => x.Populations)
              .Cascade
              .AllDeleteOrphan();
        }
    }
}
