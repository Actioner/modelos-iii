using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using SharpArch.Domain.Commands;
using BE.ModelosIII.Tasks.Commands.Configuration;

namespace BE.ModelosIII.Tasks.Commands.Scenario
{
    public class ScenarioCommand : CommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float BinSize { get; set; }
        public ConfigurationCommand Configuration { get; set; }
        public IList<ItemCommand> Items { get; set; }
    }
}