using System.Collections.Generic;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Models.Report
{
    public class MostSoldHourListModel
    {
        public MostSoldCommand Search { get; set; }
        public IList<MostSoldHourReportModel> ReportRows { get; set; }

        public MostSoldHourListModel()
        {
            Search = new MostSoldCommand();
            ReportRows = new List<MostSoldHourReportModel>();
        }
    }
}