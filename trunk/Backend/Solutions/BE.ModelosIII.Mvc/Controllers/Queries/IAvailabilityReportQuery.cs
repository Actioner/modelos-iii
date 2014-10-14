using BE.ModelosIII.Domain;
using BE.ModelosIII.Mvc.Models.Report;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public interface IAvailabilityReportQuery
    {
        AvailabilityReportModel GetReport(Screening screening);
    }
}