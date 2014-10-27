using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class ScenarioOverride : IAutoMappingOverride<Scenario>
    {
        public void Override(AutoMapping<Scenario> mapping)
        {
            mapping.HasMany(x => x.Items)
                .Cascade
                .AllDeleteOrphan();


            mapping.HasMany(x => x.Runs)
                .Cascade
                .AllDeleteOrphan();
        }
    }
}
