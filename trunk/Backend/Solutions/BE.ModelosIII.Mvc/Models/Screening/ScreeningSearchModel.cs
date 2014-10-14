using System;

namespace BE.ModelosIII.Mvc.Models.Screening
{
    public class ScreeningSearchModel
    {
        public int MultiplexId { get; set; }
        public int ScreenId { get; set; }
        public DateTime? Date { get; set; }
    }
}