﻿using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Scenario : Entity
    {
        [DomainSignature]
        public virtual string Name { get; set; }
        public virtual float BinSize { get; set; }
        public virtual IList<Item> Items { get; set; }
    }
}