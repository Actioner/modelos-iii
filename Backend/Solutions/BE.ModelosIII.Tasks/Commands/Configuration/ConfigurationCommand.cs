using BE.ModelosIII.Domain;
using SharpArch.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE.ModelosIII.Tasks.Commands.Configuration
{
    public class ConfigurationCommand : CommandBase
    {
        public int ScenarioId { get; set; }
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }
        public int PopulationSize { get; set; }
        public int StopDepth { get; set; }
        public bool Report { get; set; }
        public Enums.StopCriterion StopCriterion { get; set; }
    }
}
