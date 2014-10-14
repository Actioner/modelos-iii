using System;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class User : Entity
    {
        [DomainSignature]
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual Role Role { get; set; }
    }
}