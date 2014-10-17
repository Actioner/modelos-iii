using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Generation : Entity
    {
        public virtual Run Run { get; set; }
        public virtual IList<Population> Populations { get; set; }
        public virtual int Number { get; set; }
    }
}