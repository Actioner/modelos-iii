using System;
using System.Collections.Generic;

namespace BE.ModelosIII.Mvc.Models.Screening
{
    public class ScreeningListModel
    {
        public ScreeningSearchModel ScreeningSearch { get; set; }
        public IEnumerable<ScreeningModel> Screenings { get; set; }

        public ScreeningListModel()
        {
            ScreeningSearch = new ScreeningSearchModel();
        }
    }
}