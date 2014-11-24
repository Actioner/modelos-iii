using BE.ModelosIII.Mvc.Models.Run;
using BE.ModelosIII.Tasks.Commands.Report;
using System.Collections.Generic;

namespace BE.ModelosIII.Mvc.Models.Report
{
    public class GenerationReportListModel
    {
        public GenerationReportSearchCommand Search { get; set; }
        public IList<GenerationReportModel> ReportRows { get; set; }

        public GenerationReportListModel()
        {
            Search = new GenerationReportSearchCommand();
            ReportRows = new List<GenerationReportModel>();
        }
    }
}