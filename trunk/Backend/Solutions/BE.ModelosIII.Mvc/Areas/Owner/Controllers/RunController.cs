using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Models.Run;
using BE.ModelosIII.Tasks.Commands.Run;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SharpArch.NHibernate.Web.Mvc;
using BE.ModelosIII.Mvc.Models.Scenario;
using BE.ModelosIII.Mvc.Models.Population;
using BE.ModelosIII.Mvc.Components.Utils;
using BE.ModelosIII.Mvc.Controllers.Queries;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class RunController : BaseController
    {
        private readonly IRunRepository _runRepository;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IPopulationRepository _populationRepository;
        private readonly IMappingEngine _mappingEngine;
        private readonly IFastDeleter _fastDeleteQuery;

        public RunController(
            IRunRepository runRepository,
            IScenarioRepository scenarioRepository,
            IPopulationRepository populationRepository,
            IFastDeleter fastDeleteQuery,
            IMappingEngine mappingEngine)
        {
            _runRepository = runRepository;
            _scenarioRepository = scenarioRepository;
            _populationRepository = populationRepository;
            _fastDeleteQuery = fastDeleteQuery;
            _mappingEngine = mappingEngine;
        }

        public ActionResult Index(RunSearchModel searchModel)
        {
            var scenario = _scenarioRepository.Get(searchModel.ScenarioId);
            var runs = scenario != null
                                ? _runRepository.GetByScenarioAndDates(scenario, searchModel.UtcStart, searchModel.UtcEnd)
                                    .OrderByDescending(r => r.RunOn).ToList()
                                : _runRepository.GetByDates(searchModel.UtcStart, searchModel.UtcEnd)
                                    .OrderByDescending(r => r.RunOn)
                                    .ThenBy(r => r.Scenario.Name).ToList();

            var runModels = _mappingEngine.Map<IList<RunListItemModel>>(runs
                .OrderByDescending(r => r.RunOn));

            var runListModel = new RunListModel
            {
                Runs = runModels,
                RunSearch = searchModel
            };
            
            BindIndexValues();

            return View(runListModel);
        }

        [HttpGet]
        public ActionResult Run(RunScenarioCommand command)
        {
            if (command.ScenarioId < 1 || _scenarioRepository.Get(command.ScenarioId) == null) 
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index");
            }

            int runId = new RunSolutionForScenario().Execute(command.ScenarioId);
            bool success = runId > 0;

            if (Request.IsAjaxRequest())
            {
                return Json(new { success = success, runId = runId, redirectUrl = Url.Action("View", new { id = runId }) }, JsonRequestBehavior.AllowGet);
            }

            if (success)
            {
                return RedirectToAction("View", new { id = runId });
            }
            return RedirectToAction("Error", "Error");
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            var run = _runRepository.Get(id);
            if (run == null)
            {
                return RedirectToAction("Index");
            }

            var bestSolution = _populationRepository.FindBestByRun(run);

            BindValues();
            var model = _mappingEngine.Map<Run, RunViewModel>(run);
            model.Population = _mappingEngine.Map<Population, PopulationViewModel>(bestSolution);

            return View(model);
        }


        [HttpGet]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult Delete(DeleteRunCommand command)
        {
            //_commandProcessor.Process(command);
            _fastDeleteQuery.DeleteRun(command);
            return RedirectToAction("Index");
        }


        private void BindIndexValues()
        {
            var scenarios = _scenarioRepository.GetAll().OrderBy(m => m.Name);
            ViewBag.Scenarios = _mappingEngine.Map<IList<ScenarioListModel>>(scenarios);
        }

        private void BindValues()
        {
        }
    }
}
