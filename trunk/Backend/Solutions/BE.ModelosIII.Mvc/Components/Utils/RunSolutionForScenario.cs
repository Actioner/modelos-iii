using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE.ModelosIII.Mvc.Components.Utils
{
    public class RunSolutionForScenario
    {
        public int Execute(int scenarioId) 
        {
            string filename = HttpContext.Current.Server.MapPath("~/Galex/App10.exe");

            var procStartInfo = new System.Diagnostics.ProcessStartInfo(filename, scenarioId.ToString());
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            var process = new System.Diagnostics.Process();
            process.StartInfo = procStartInfo;
            process.Start();

            process.WaitForExit();

            return process.ExitCode;
        }
    }
}