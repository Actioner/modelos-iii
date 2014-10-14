using System;
using System.Collections.Generic;
using System.ComponentModel;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Promotion : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }
        public virtual string Poster { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual PromotionType Type { get; set; }
        public virtual IList<DayOfWeek> Days { get; set; }
        public virtual bool Active { get; set; }


        public Promotion()
        {
            Days = new List<DayOfWeek>();
        }
    }

    public enum PromotionType
    {
        [Description("Precio Fijo")]
        FixedPrice = 1,
        [Description("NxM")]
        NxM,
        [Description("Porcentaje")]
        Percent,
    }
}