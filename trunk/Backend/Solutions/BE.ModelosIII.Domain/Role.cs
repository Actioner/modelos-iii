using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Role : Entity
    {
        public virtual string Name { get; set; }
    }
}