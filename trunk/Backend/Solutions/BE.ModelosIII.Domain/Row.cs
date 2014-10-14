using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Row : Entity
    {
        public virtual string Name { get; set; }
        public virtual Screen Screen { get; set; }
        public virtual IList<Seat> Seats { get; set; }
    }
}