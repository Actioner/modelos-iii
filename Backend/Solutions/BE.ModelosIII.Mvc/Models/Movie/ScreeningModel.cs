using System;

namespace BE.ModelosIII.Mvc.Models.Movie
{
    public class ScreeningModel
    {
        public int Id { get; set; }
        public string Screen { get; set; }
        public DateTime StartDate { get; set; }
    }
}