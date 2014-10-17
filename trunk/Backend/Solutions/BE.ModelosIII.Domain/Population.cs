using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Population : Entity
    {
        public virtual Generation Generation { get; set; }
        public virtual IList<Bin> Bins { get; set; }
        public virtual int Number { get; set; }
        public virtual float Fitness { get; set; }
        public virtual int BinCount { get; set; }
    }
}