using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE.ModelosIII.Tasks.Commands.Scenario
{
    public class ItemCommand
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int Quantity { get; set; }
        public float Size { get; set; }
    }
}
