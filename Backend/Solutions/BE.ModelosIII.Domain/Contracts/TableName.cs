using System;

namespace BE.ModelosIII.Domain.Contracts
{
    public class TableNameAttribute : Attribute
    {
        public string Value { get; private set; }

        public TableNameAttribute(string multiplexes)
        {
            this.Value = multiplexes;
        }
    }
}
