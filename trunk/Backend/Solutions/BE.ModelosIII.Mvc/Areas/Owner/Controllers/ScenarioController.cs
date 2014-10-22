using System.Web.Routing;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Models.Scenario;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.Domain.Commands;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Web.Mvc;
using BE.ModelosIII.Mvc.Models.Item;
using BE.ModelosIII.Mvc.Validators.Scenario;
using FluentValidation.Mvc;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class ScenarioController : BaseController
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IMappingEngine _mappingEngine;

        public ScenarioController(
            ICommandProcessor commandProcessor,
            IScenarioRepository scenarioRepository,
            IMappingEngine mappingEngine)
        {

            _commandProcessor = commandProcessor;
            _scenarioRepository = scenarioRepository;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Index/
        public ActionResult Index()
        {
            var scenarios = _scenarioRepository.GetAll();
            var scenarioModels = _mappingEngine.Map<IList<ScenarioListModel>>(scenarios);

            ViewBag.Actions = new List<ActionModel>
                                  {
                                      new ActionModel("Crear", "Create", IconType.Create),
                                  };

            return View(scenarioModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BindValues();

            return View(new CreateScenarioCommand());
        }

        [HttpPost]
        [Transaction]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreateScenarioCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true, isValid = true, action = Url.Action("Index") });
                }   
                return RedirectToAction("Index");
            }

            BindValues();

            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true, isValid = false, html = RenderPartialViewToString("_Create", command) });
            }
            return View(command);
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            var scenario = _scenarioRepository.Get(id);
            if (scenario == null)
            {
                return RedirectToAction("Index");
            }

            BindValues();
            var command = _mappingEngine.Map<Scenario, EditScenarioCommand>(scenario);

            return View(command);
        }


        [HttpGet]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult Delete(DeleteScenarioCommand command)
        {
            _commandProcessor.Process(command);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ValidateNewItem(ItemModel model)
        {
            BindValues();

            var validator = new ItemValidator();
            var result = validator.Validate(model);
            result.AddToModelState(ModelState, (string)ViewBag.NewItemPrefix);

            return Json(new { success = ModelState.IsValid, html = RenderPartialViewToString("_NewItem", model, (string)ViewBag.NewItemPrefix) });
        }

        private void BindValues()
        {
            ViewBag.NewItemPrefix = "NewItem";
        }
    }
}
