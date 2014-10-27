﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.ModelosIII.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BE.ModelosIII.Infrastructure.NHibernateMaps.Overrides
{
    public class Binverride : IAutoMappingOverride<Bin>
    {
        public void Override(AutoMapping<Bin> mapping)
        {
            mapping.HasMany(x => x.BinItems)
             .Cascade
             .AllDeleteOrphan()
             .Inverse();
        }
    }
}
