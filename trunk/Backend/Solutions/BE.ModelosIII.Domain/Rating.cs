using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Rating : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}