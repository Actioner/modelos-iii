using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Mvc.Models.Run;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public interface IGenerationReportQuery
    {
        IList<GenerationReportModel> GetReport(GenerationReportSearchCommand search);
    }
}