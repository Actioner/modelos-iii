using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public interface IMostSoldHourReportQuery
    {
        IList<MostSoldHourReportModel> GetReport(MostSoldCommand search);
    }
}