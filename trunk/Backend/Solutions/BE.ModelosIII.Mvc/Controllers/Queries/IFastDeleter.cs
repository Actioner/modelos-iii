using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.ModelosIII.Tasks.Commands.Run;
using BE.ModelosIII.Tasks.Commands.Scenario;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public interface IFastDeleter
    {
        void DeleteRun(DeleteRunCommand command);
        void DeleteScenario(DeleteScenarioCommand command);
    }
}