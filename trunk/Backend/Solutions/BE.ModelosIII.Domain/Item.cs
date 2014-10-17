using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Item : Entity
    {
        [DomainSignature]
        public virtual string Label { get; set; }
        public virtual Scenario Scenario { get; set; }
        public virtual int Quantity { get; set; }
        public virtual float Size { get; set; }
    }
}