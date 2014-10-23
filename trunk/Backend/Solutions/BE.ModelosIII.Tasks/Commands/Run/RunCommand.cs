using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Run
{
    public class RunCommand : CommandBase
    {
        public int Id { get; set; }
    }
}