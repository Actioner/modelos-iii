using System.Collections.Generic;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Models.Report
{
    public class MostSoldMovieListModel
    {
        public MostSoldCommand Search { get; set; }
        public IList<MostSoldMovieReportModel> ReportRows { get; set; }

        public MostSoldMovieListModel()
        {
            Search = new MostSoldCommand();
            ReportRows = new List<MostSoldMovieReportModel>();
        }
    }
}