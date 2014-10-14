using System;
using System.Collections.Generic;
using BE.ModelosIII.Infrastructure.Helpers;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Screening
{
    public class AssignScreeningCommand : CommandBase
    {
        public string StartTimes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MultiplexId { get; set; }
        public int ScreenId { get; set; }
        public int MovieId { get; set; }
        public IList<DayOfWeek> Days { get; set; }

        public IList<TimeSpan> StartTimeSpans { get; set; } 
        public IList<Domain.Screening> Screenings { get; set; } 

        public AssignScreeningCommand()
        {
            Days = new List<DayOfWeek>();
            StartTimeSpans = new List<TimeSpan>();
            Screenings = new List<Domain.Screening>();
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
        }
    }
}
