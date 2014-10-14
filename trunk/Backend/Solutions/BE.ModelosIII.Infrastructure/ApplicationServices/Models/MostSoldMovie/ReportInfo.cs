using System.Collections.Generic;

namespace BE.ModelosIII.Infrastructure.ApplicationServices.Models.MostSoldMovie
{
    public class ReportInfo
    {
        public string Title { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Multiplex { get; set; }

        public IEnumerable<ReportDataItem> Items { get; set; }

        public ReportInfo()
        {
            Items = new List<ReportDataItem>();
        }
    }
}
