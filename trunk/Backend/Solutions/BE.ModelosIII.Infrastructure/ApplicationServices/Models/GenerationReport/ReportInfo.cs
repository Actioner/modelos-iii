using System.Collections.Generic;

namespace BE.ModelosIII.Infrastructure.ApplicationServices.Models.GenerationReport
{
    public class ReportInfo
    {
        public string Title { get; set; }
        public string Run { get; set; }
        public string ChartUri { get; set; }

        public IEnumerable<ReportDataItem> Items { get; set; }

        public ReportInfo()
        {
            Items = new List<ReportDataItem>();
        }
    }
}
