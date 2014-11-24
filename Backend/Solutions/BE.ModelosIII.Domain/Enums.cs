using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace BE.ModelosIII.Domain
{
    public static class Enums
    {
        public enum StopCriterion 
        {
            [Description("Cambio de fitness")]
            StatsChange = 0,
            [Description("Cuenta de generación")]
            Count
        }
    }
}
