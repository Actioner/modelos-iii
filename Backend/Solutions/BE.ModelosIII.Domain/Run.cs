using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Run : Entity
    {
        public virtual Scenario Scenario { get; set; }
        public virtual IList<Generation> Generations { get; set; }
        public virtual System.DateTime RunOn { get; set; }
    }
}