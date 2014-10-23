using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE.ModelosIII.Mvc.Models.Run
{
    public class RunSearchModel
    {
        public int ScenarioId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}