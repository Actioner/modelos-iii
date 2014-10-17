using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Bin : Entity
    {
        public virtual Population Population { get; set; }
        public virtual IList<BinItem> BinItems { get; set; }
        public virtual float Filled { get; set; }
        public virtual float Capacity { get; set; }
    }
}