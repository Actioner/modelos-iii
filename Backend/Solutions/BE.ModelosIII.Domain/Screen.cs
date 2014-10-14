using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Screen : Entity
    {
        [DomainSignature]
        public virtual string Name { get; set; }
        public virtual int Capacity { get; set; }
        public virtual Multiplex Multiplex { get; set; }
        //[LongText]
        //public virtual string Layout { get; set; }
        public virtual IList<Screening> Screenings { get; set; }
        public virtual IList<Row> Rows { get; set; }
    }
}