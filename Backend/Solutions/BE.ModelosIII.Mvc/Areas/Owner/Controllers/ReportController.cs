using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.ApplicationServices;
using BE.ModelosIII.Mvc.Controllers;
using System.Web.Mvc;
using BE.ModelosIII.Mvc.Controllers.Queries;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Tasks.Commands.Report;
using BE.ModelosIII.Mvc.Models.Scenario;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Resources;
using System;
using System.IO;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IGenerationReportQuery _generationReportQuery;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IRunRepository _runRepository;
        private readonly IPdfManager _pdfManager;
        private readonly IMappingEngine _mappingEngine;

        public ReportController(
            IGenerationReportQuery generationReportQuery,
            IScenarioRepository scenarioRepository,
            IRunRepository runRepository,
            IPdfManager pdfManager,
            IMappingEngine mappingEngine)
        {
            _generationReportQuery = generationReportQuery;
            _scenarioRepository = scenarioRepository;
            _runRepository = runRepository;
            _pdfManager = pdfManager;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Generation/
        [HttpGet]
        public ActionResult Generation()
        {
            BindSearchValues();
            return View("Index", new GenerationReportListModel());
        }

        //
        // POST: /Generation/
        [HttpPost]
        public ActionResult Generation(GenerationReportSearchCommand search)
        {
            BindSearchValues();
            var result = new GenerationReportListModel
                             {
                                 Search = search
                             };

            if (ModelState.IsValid)
            {
                result.ReportRows = _generationReportQuery.GetReport(search);
                var action = new ActionModel("Exportar", "GenerationExport", IconType.Create, "export")
                                 {
                                     RouteValues = new
                                                       {
                                                           scenarioId = search.ScenarioId,
                                                           runId = search.RunId
                                                       }
                                 };
                ViewBag.Actions = new List<ActionModel>
                                  {
                                      action,
                                  };
            }

            return View("Index", result);
        }

        //
        // POST: /Generation/
        [HttpPost]
        public JsonResult GenerationExport(GenerationReportSearchCommand search)
        {
            var reportRows = _generationReportQuery.GetReport(search);
            var run = _runRepository.Get(search.RunId);

            var reportInfo = new Infrastructure.ApplicationServices.Models.GenerationReport.ReportInfo
                                 {
                                     Title = ReportMessages.GenerationTitle,
                                     Run = run.Scenario.Name + " (" + run.RunOn + ")",
                                     ChartUri = search.ChartUri,
                                     Items = _mappingEngine.Map<IList<Infrastructure.ApplicationServices.Models.GenerationReport.ReportDataItem>>(reportRows)
                                 };

            string reportFile = string.Format("{0}_{1}.pdf", ReportMessages.GenerationTitle, DateTime.Now.ToString("yyyyMMddHHmmss"));
            string fullPath = Path.Combine(Server.MapPath("~/uploads/reports"), reportFile);
            var pdfData = _pdfManager.GetGenerationReportContent(reportInfo);

            System.IO.File.WriteAllBytes(fullPath, pdfData);
            return Json(new { success = true, reportFile = reportFile });
        }

        [HttpGet]
        public FileContentResult DownloadGenerationReport(string reportFile)
        {
            string fullPath = Path.Combine(Server.MapPath("~/uploads/reports"), reportFile);
            var result = System.IO.File.ReadAllBytes(fullPath);

            return File(result, "application/pdf", reportFile);
        }

        private void BindSearchValues()
        {
            var scenarios = _scenarioRepository.GetAll().OrderBy(m => m.Name);
            ViewBag.Scenarios = _mappingEngine.Map<IList<ScenarioListModel>>(scenarios);
            ViewBag.Runs = scenarios.Select(m => new
                                                     {
                                                         ScenarioId = m.Id,
                                                         Runs = m.Runs.Select(s => new { s.Id, RunOnUtc = s.RunOn.ToString("o") })
                                                     }).ToJson();
        }
    }
}