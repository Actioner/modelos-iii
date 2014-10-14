using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public interface IMostSoldMovieReportQuery
    {
        IList<MostSoldMovieReportModel> GetReport(MostSoldCommand search);
    }
}