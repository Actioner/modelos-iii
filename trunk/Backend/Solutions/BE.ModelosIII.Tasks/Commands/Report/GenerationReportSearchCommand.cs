using BE.ModelosIII.Domain;
using SharpArch.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE.ModelosIII.Tasks.Commands.Report
{
    public class GenerationReportSearchCommand : CommandBase
    {
        public int ScenarioId { get; set; }
        public int RunId { get; set; }
        public string ChartUri { get; set; }
    }
}
