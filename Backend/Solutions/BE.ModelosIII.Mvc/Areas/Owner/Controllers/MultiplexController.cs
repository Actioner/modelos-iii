using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Models.Multiplex;
using BE.ModelosIII.Mvc.Models.Screen;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using SharpArch.Domain.Commands;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Web.Mvc;
using System.Xml;
using System;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class MultiplexController : BaseController
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IRepository<Multiplex> _multiplexRepository;
        private readonly IScreenRepository _screenRepository;
        private readonly IMappingEngine _mappingEngine;

        public MultiplexController(
            ICommandProcessor commandProcessor,
            IRepository<Multiplex> multiplexRepository,
            IScreenRepository screenRepository,
            IMappingEngine mappingEngine)
        {

            _commandProcessor = commandProcessor;
            _multiplexRepository = multiplexRepository;
            _screenRepository = screenRepository;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Index/
        public ActionResult Index()
        {
            var multiplexs = _multiplexRepository.GetAll();
            var multiplexModels = _mappingEngine.Map<IList<MultiplexModel>>(multiplexs);

            ViewBag.Actions = new List<ActionModel>
                                  {
                                      new ActionModel("Crear", "Create", IconType.Create),
                                  };

            return View(multiplexModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateMultiplexCommand());
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMultiplexCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("Index");
            }

            return View(command);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var multiplex = _multiplexRepository.Get(id);
            if (multiplex == null)
            {
                return RedirectToAction("Index");
            }
            var command = _mappingEngine.Map<Multiplex, EditMultiplexCommand>(multiplex);

            return View(command);
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMultiplexCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("Index");
            }

            return View(command);
        }

        [HttpGet]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult Delete(DeleteMultiplexCommand command)
        {
            _commandProcessor.Process(command);
            return RedirectToAction("Index");
        }

        //
        // GET: /Index/
        public ActionResult ScreenIndex(int id)
        {
            var multiplex = _multiplexRepository.Get(id);
            if (multiplex == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var screens = _screenRepository.GetByMultiplex(multiplex);
            var models = _mappingEngine.Map<IList<ScreenModel>>(screens);

            var actionModel = new ActionModel("Crear", "ScreenCreate", IconType.Create);
            actionModel.RouteValues = new { multiplexId = id };
            ViewBag.Actions = new List<ActionModel>
                                  {
                                      actionModel,
                                  };

            ViewBag.Multiplex = multiplex.Name;

            return View("Screen/Index", models);
        }

        [HttpGet]
        public ActionResult ScreenCreate(int multiplexId)
        {
            var multiplex = _multiplexRepository.Get(multiplexId);
            if (multiplex == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Multiplex = multiplex.Name;

            return View("Screen/Create", new CreateScreenCommand
                            {
                                MultiplexId = multiplexId
                            });
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult ScreenCreate(CreateScreenCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("ScreenIndex", new { id = command.MultiplexId });
            }

            var multiplex = _multiplexRepository.Get(command.MultiplexId);
            ViewBag.Multiplex = multiplex.Name;

            return View("Screen/Create", command);
        }

        [HttpGet]
        public ActionResult ScreenEdit(int id)
        {
            var screen = _screenRepository.Get(id);
            if (screen == null)
            {
                return RedirectToAction("Index");
            }
            var command = _mappingEngine.Map<Screen, EditScreenCommand>(screen);


            var multiplex = _multiplexRepository.Get(command.MultiplexId);
            ViewBag.Multiplex = multiplex.Name;

            return View("Screen/Edit", command);
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult ScreenEdit(EditScreenCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("ScreenIndex", new { id = command.MultiplexId });
            }

            var multiplex = _multiplexRepository.Get(command.MultiplexId);
            ViewBag.Multiplex = multiplex.Name;

            return View("Screen/Edit", command);
        }

        [HttpGet]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult ScreenDelete(DeleteScreenCommand command)
        {
            _commandProcessor.Process(command);
            return RedirectToAction("ScreenIndex", new { id = command.MultiplexId });
        }

        [HttpGet]
        public ActionResult ScreenMap(string xml)
        {
            var model = new ScreenMapModel();
            ViewBag.Prefix = "Rows";

            try
            {
                var doc = new XmlDocument();
                doc.Load(xml);
                model.ValidateSchema(xml, Server.MapPath(Url.Content("~/Content/schemas/screen_schema.xsd")));
                model.InitRowsFromXml(doc);
                ViewBag.Error = model.Error;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error en el formato del XML";
            }

            return PartialView("_ScreenMap", new CreateScreenCommand
                                                 {
                                                     Rows = model.Rows
                                                 });
        }

        [HttpGet]
        public JsonResult GetPosibleDates(int id)
        {
            var screen = _screenRepository.Get(id);
            if (screen == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var result = screen.Screenings
                .Select(sc => sc.StartDate.ToString("dd/MM/yyyy"))
                .Distinct();

            return Json(new { success = true, dates = result }, JsonRequestBehavior.AllowGet);

        }
    }
}
