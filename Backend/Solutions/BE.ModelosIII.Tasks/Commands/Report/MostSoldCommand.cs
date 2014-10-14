using System;

namespace BE.ModelosIII.Tasks.Commands.Report
{
    public class MostSoldCommand
    {
        public int? MultiplexId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
