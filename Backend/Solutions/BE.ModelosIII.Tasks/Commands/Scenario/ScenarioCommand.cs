using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Scenario
{
    public class ScenarioCommand : CommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float BinSize { get; set; }
        public IList<Item> Items { get; set; }
    }
}