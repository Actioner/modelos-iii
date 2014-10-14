using System;
using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;

namespace BE.ModelosIII.Mvc.Models.Screening
{
    public class ScreeningModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Screen { get; set; }
        public string Multiplex { get; set; }
        public string Movie { get; set; }

        public AvailabilityReportModel Report { get; set; }
        public IList<RowModel> Rows { get; set; }

        public ScreeningModel()
        {
            Rows = new List<RowModel>();
        }
    }
}