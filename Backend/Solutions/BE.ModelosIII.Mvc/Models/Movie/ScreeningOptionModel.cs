using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Multiplex;

namespace BE.ModelosIII.Mvc.Models.Movie
{
    public class ScreeningOptionModel
    {
        public MultiplexModel Multiplex { get; set; }
        public IList<ScreeningModel> Screenings { get; set; }
    }
}