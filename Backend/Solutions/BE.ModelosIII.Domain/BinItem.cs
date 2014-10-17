using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class BinItem : Entity
    {
        public virtual Bin Bin { get; set; }
        public virtual string Label { get; set; }
        public virtual float Size { get; set; }
    }
}