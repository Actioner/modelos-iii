using System;
using System.Collections.Generic;
using BE.ModelosIII.Tasks.Commands.Utility;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Movie
{
    public class MovieCommand : CommandBase
    {
        public int Id { get; set; }
        public int RatingId { get; set; }
        public IList<int> GenreIds { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public string MainCast { get; set; }
        public string Trailer { get; set; }
        public string Synopsis { get; set; }
        public string SmallPoster { get; set; }
        public int Runtime { get; set; }
        public FileUploadModel NewSmallPoster { get; set; }
        public string Poster { get; set; }
        public FileUploadModel NewPoster { get; set; }

        public MovieCommand()
        {
            NewPoster = new FileUploadModel
                             {
                                 Id = "NewPoster"
                             };

            NewSmallPoster = new FileUploadModel
                             {
                                 Id = "NewSmallPoster"
                             };
        }
    }
}