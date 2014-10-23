using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class BinItemOverride : IAutoMappingOverride<BinItem>
    {
        public void Override(AutoMapping<BinItem> mapping)
        {
            mapping.References(x => x.Bin)
               .Cascade
               .None();
        }
    }
}
