using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Run
{
    public class RunScenarioCommand : CommandBase
    {
        public int ScenarioId { get; set; }
    }
}