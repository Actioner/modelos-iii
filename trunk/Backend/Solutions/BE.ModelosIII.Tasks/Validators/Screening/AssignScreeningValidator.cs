using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Screening;
using FluentValidation;

namespace BE.ModelosIII.Tasks.Validators.Screening
{
    public class AssignScreeningValidator : AbstractValidator<AssignScreeningCommand>
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IScreenRepository _screenRepository;
        private readonly IMovieRepository _movieRepository;

        public AssignScreeningValidator(
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
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithLocalizedName(() => PropertyNames.StartDate);

            RuleFor(x => x.EndDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithLocalizedName(() => PropertyNames.EndDate);
            
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

            RuleFor(x => x.Days)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithLocalizedName(() => PropertyNames.Days);
            
            RuleFor(x => x.StartTimes)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Must((x, y) => BeValidTime(x))
                .Must((x, y) => NotOverlap(x)).WithLocalizedMessage(() => ValidationMessages.ScreeningOverlap)
                .WithLocalizedName(() => PropertyNames.StartTime);
        }

        private bool NotOverlap(AssignScreeningCommand command)
        {
            var screenings = GetPossibleScreenings(command);

            if (!screenings.Any())
            {
                return true;
            }
            if (AssignCollision(screenings))
            {
                return false;
            }

            if (AssignOverlapping(screenings))
            {
                return false;
            }

            command.Screenings = screenings;
            return true;
            
        }

        private bool AssignOverlapping(IList<Domain.Screening> screenings)
        {
            foreach (var screening in screenings)
            {
                var potentialOverlappings = _screeningRepository.GetByScreenAndDateRange(screening.Screen, screening.StartDate, screening.EndDate);
                if (potentialOverlappings.Any())
                {
                    return true;
                }
            }
            return false;
        }

        private bool AssignCollision(IList<Domain.Screening> screenings)
        {
            for (int i = 0; i < screenings.Count/2 + 1; i++)
            {
                for (int j = i + 1; j < screenings.Count; j++)
                {
                    if (screenings[i].StartDate < screenings[j].EndDate
                        && screenings[i].EndDate > screenings[j].StartDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool BeValidTime(AssignScreeningCommand command)
        {
            var startTimesList = command.StartTimes.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (!startTimesList.Any())
            {
                return false;
            }

            foreach (var startTime in startTimesList)
            {
                DateTime time;
                if (!DateTime.TryParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    return false;
                }

                command.StartTimeSpans.Add(time.TimeOfDay);
            }
            return true;
        }

        
        private IList<Domain.Screening> GetPossibleScreenings(AssignScreeningCommand command)
        {
            var screenings = new List<Domain.Screening>();
            var movie = _movieRepository.Get(command.MovieId);
            if (movie == null)
            {
                return screenings;
            }
            var screen = _screenRepository.Get(command.ScreenId);
            if (screen == null)
            {
                return screenings;
            }
            var startTimes = command.StartTimeSpans.OrderBy(st => st);
            var startDate = command.StartDate.Date;
            var endDate = command.EndDate.Date.AddDays(1);

            foreach (var startTime in startTimes)
            {
                var auxStart = startDate;
                while (auxStart < endDate)
                {
                    if (command.Days.Contains(auxStart.DayOfWeek))
                    {
                        var screening = new Domain.Screening
                                            {
                                                Movie = movie,
                                                Screen = screen,
                                                StartDate = auxStart.Date + startTime
                                            };
                        screenings.Add(screening);
                    }
                    auxStart = auxStart.AddDays(1);
                }
            }
            return screenings;
        }
    }
}
