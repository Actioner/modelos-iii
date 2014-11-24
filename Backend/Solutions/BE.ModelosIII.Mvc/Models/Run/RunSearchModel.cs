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
        public int OffsetMinutes { get; set; }

        public DateTime? UtcStart {
            get
            {
                if (!Start.HasValue)
                    return null;

                return Start.Value.AddMinutes(OffsetMinutes);
            }
        }
        public DateTime? UtcEnd
        {
            get
            {
                if (!End.HasValue)
                    return null;

                return End.Value.AddDays(1).AddMinutes(OffsetMinutes);
            }
        }
    }
}