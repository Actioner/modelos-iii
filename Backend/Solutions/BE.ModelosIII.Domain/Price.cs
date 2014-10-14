using System.ComponentModel;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Price : Entity
    {
        public virtual double Value { get; set; }
        public virtual PriceType Type { get; set; }
    }

    public enum PriceType
    {
        [Description("General")]
        General,
    }
}