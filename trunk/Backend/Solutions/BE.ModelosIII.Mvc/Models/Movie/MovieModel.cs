using System.Collections.Generic;

namespace BE.ModelosIII.Mvc.Models.Movie
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public string MainCast { get; set; }
        public string Trailer { get; set; }
        public string Synopsis { get; set; }
        public string SmallPoster { get; set; }
        public string Poster { get; set; }
        public int Runtime { get; set; }
        public RatingModel Rating { get; set; }
        public IList<ScreeningOptionModel> ScreeningOptions { get; set; }
        public string Genres { get; set; }

        public MovieModel()
        {
            ScreeningOptions = new List<ScreeningOptionModel>();
        }
    }
}