using System;
using System.Net;
using System.Text.RegularExpressions;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Movie;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Movie
{
    public abstract class CreateOrEditMovieValidator<T> : AbstractValidator<T>
        where T : MovieCommand
    {
        protected CreateOrEditMovieValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Title);

            RuleFor(x => x.OriginalTitle)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.OriginalTitle);

            RuleFor(x => x.Director)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Director);

            RuleFor(x => x.MainCast)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Cast);

            RuleFor(x => x.Runtime)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Runtime);

            RuleFor(x => x.YearOfRelease)
                .GreaterThan(1900)
                .LessThanOrEqualTo(DateTime.UtcNow.Year)
                .WithLocalizedName(() => PropertyNames.YearOfRelease);

            RuleFor(x => x.Trailer)
                .Must((x, y) => BeValidYoutubeVideo(y))
                .WithLocalizedName(() => PropertyNames.Trailer);

            RuleFor(x => x.Synopsis)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Synopsis);

            RuleFor(x => x.RatingId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Rating);

            RuleFor(x => x.GenreIds)
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Genre);
        }

        private bool BeValidYoutubeVideo(string videoUrl)
        {
            if (string.IsNullOrEmpty(videoUrl))
            {
                return true;
            }
            
            Uri uri;
            if (!Uri.TryCreate(videoUrl, UriKind.Absolute, out uri))
            {
                return false;
            }
            var match = Regex.Match(videoUrl, @"^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!match.Success || match.Groups[7].Length != 11)
            {
                return false;
            }

            string videoId = match.Groups[7].Value;
            var targetUri = new Uri("http://gdata.youtube.com/feeds/api/videos/" + videoId);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(targetUri);
                var response = request.GetResponse() as HttpWebResponse;
                return response != null && response.StatusCode == HttpStatusCode.OK;
            }
            catch(WebException)
            {
                return false;
            }
        }
    }
}
