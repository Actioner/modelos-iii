using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Screening;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Screening
{
    public class EditScreeningValidator : AbstractValidator<EditScreeningCommand>
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IScreenRepository _screenRepository;
        private readonly IMovieRepository _movieRepository;

        public EditScreeningValidator(
            IScreeningRepository screeningRepository,
            IScreenRepository screenRepository,
            IMovieRepository movieRepository)
        {
            _screeningRepository = screeningRepository;
            _screenRepository = screenRepository;
            _movieRepository = movieRepository;

            RuleFor(x => x.StartDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Date);

            RuleFor(x => x.MultiplexId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Multiplex);

            RuleFor(x => x.ScreenId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Screen);

            RuleFor(x => x.MovieId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithLocalizedName(() => PropertyNames.Movie);
            
            RuleFor(x => x.StartTime)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Must((x, y) => BeValidTime(y))
                .Must((x, y) => NotOverlap(x)).WithLocalizedMessage(() => ValidationMessages.ScreeningOverlap)
                .WithLocalizedName(() => PropertyNames.StartTime);
        }

        private bool NotOverlap(EditScreeningCommand editScreeningCommand)
        {
            var movie = _movieRepository.Get(editScreeningCommand.MovieId);
            if (movie == null)
            {
                return true;
            }

            var screen = _screenRepository.Get(editScreeningCommand.ScreenId);
            if (screen == null)
            {
                return true;
            }
            var startTime = DateTime.ParseExact(editScreeningCommand.StartTime, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
            var startDate = editScreeningCommand.StartDate.Date + startTime;

            var screening = new Domain.Screening
                                {
                                    Movie = movie,
                                    Screen = screen,
                                    StartDate = startDate
                                };

            var potentialOverlappings = _screeningRepository.GetByScreenAndDateRange(screening.Screen, screening.StartDate, screening.EndDate);
            return !potentialOverlappings.Any() || (potentialOverlappings.Count == 1 && potentialOverlappings.First().Id == editScreeningCommand.Id);
        }

        private bool BeValidTime(string startTime)
        {
            DateTime time;
            return DateTime.TryParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
        }
    }
}
