using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    [TableName("Multiplexes")]
    public class Multiplex : Entity
    {
        [DomainSignature]
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Subways { get; set; }
        public virtual string Buses { get; set; }
        public virtual string Poster { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual IList<Screen> Screens { get; set; }
    }
}