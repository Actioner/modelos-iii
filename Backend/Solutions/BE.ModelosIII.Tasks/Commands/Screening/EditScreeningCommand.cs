using System;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Screening
{
    public class EditScreeningCommand : CommandBase
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public int MultiplexId { get; set; }
        public int ScreenId { get; set; }
        public int MovieId { get; set; }
    }
}
