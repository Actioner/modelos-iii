using System;
using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Controllers.Queries;
using BE.ModelosIII.Mvc.Models.Movie;
using BE.ModelosIII.Mvc.Models.Multiplex;
using BE.ModelosIII.Mvc.Models.Screening;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Tasks.Commands.Screening;
using SharpArch.Domain.Commands;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Web.Mvc;
using ScreeningModel = BE.ModelosIII.Mvc.Models.Screening.ScreeningModel;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class ScreeningController : BaseController
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IScreeningRepository _screeningRepository;
        private readonly IMultiplexRepository _multiplexRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IRepository<Screen> _screenRepository;
        private readonly IMappingEngine _mappingEngine;
        private readonly IAvailabilityReportQuery _availabilityReportQuery;

        public ScreeningController(
            ICommandProcessor commandProcessor,
            IScreeningRepository screeningRepository,
            IMultiplexRepository multiplexRepository,
            IMovieRepository movieRepository,
            IRepository<Screen> screenRepository,
            IAvailabilityReportQuery availabilityReportQuery,
            IMappingEngine mappingEngine)
        {

            _commandProcessor = commandProcessor;
            _screeningRepository = screeningRepository;
            _multiplexRepository = multiplexRepository;
            _movieRepository = movieRepository;
            _screenRepository = screenRepository;
            _availabilityReportQuery = availabilityReportQuery;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Index/
        public ActionResult Index(ScreeningSearchModel searchModel)
        {
            var multiplex = _multiplexRepository.Get(searchModel.MultiplexId);
            var screen = _screenRepository.Get(searchModel.ScreenId);
            var screenings = _screeningRepository.GetByMultiplexAndScreenAndDate(multiplex, screen, searchModel.Date ?? DateTime.MinValue);
            
            var screeningModels = _mappingEngine.Map<IList<ScreeningModel>>(screenings
                .OrderByDescending(sc => sc.StartDate)
                .ThenBy(sc => sc.Screen.Name)
                .ThenBy(sc => sc.Screen.Multiplex.Name)
                .ThenBy(sc => sc.Movie.Title));

            var screeningListModel = new ScreeningListModel
            {
                Screenings = screeningModels,
                ScreeningSearch = searchModel
            };

            BindIndexValues();

            return View(screeningListModel);
        }

        private void BindIndexValues()
        {
            ViewBag.Actions = new List<ActionModel>
                                  {
                                      new ActionModel("Asignar", "Assign", IconType.Create),
                                  };
            var multiplexes = _multiplexRepository.GetAll().OrderBy(m => m.Name);
            ViewBag.Multiplexes = _mappingEngine.Map<IList<MultiplexModel>>(multiplexes);
            ViewBag.Screens = multiplexes.Select(m => new
                                                     {
                                                         MultiplexId = m.Id,
                                                         Screens = m.Screens.Select(s => new { s.Id, s.Name })
                                                     }).ToJson();
        }

        [HttpGet]
        public ActionResult Assign()
        {
            BindValues();

            return View(new AssignScreeningCommand());
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(AssignScreeningCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("Index");
            }

            BindValues();

            return View(command);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var screening = _screeningRepository.Get(id);
            if (screening == null)
            {
                return RedirectToAction("Index");
            }

            BindValues();
            var command = _mappingEngine.Map<Screening, EditScreeningCommand>(screening);

            return View(command);
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditScreeningCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("Index");
            }
            BindValues();

            return View(command);
        }

        [HttpGet]
        public ActionResult Availability(int id)
        {
            var screening = _screeningRepository.Get(id);
            if (screening == null)
            {
                return RedirectToAction("Index");
            }

            var model = _mappingEngine.Map<ScreeningModel>(screening);
            model.Report = _availabilityReportQuery.GetReport(screening);
            return View(model);
        }

        [HttpGet]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult Delete(DeleteScreeningCommand command)
        {
            _commandProcessor.Process(command);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult GetPosibleTimes(ScreeningSearchModel searchModel)
        {
            var multiplex = _multiplexRepository.Get(searchModel.MultiplexId);
            var screen = _screenRepository.Get(searchModel.ScreenId);
            var screenings = _screeningRepository.GetByMultiplexAndScreenAndDate(multiplex, screen, searchModel.Date ?? DateTime.MinValue);
            if (screenings == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var result = screenings
                .Select(sc => new
                {
                    id = sc.Id,
                    time = sc.StartDate.ToString("HH:mm")
                });

            return Json(new { success = true, times = result }, JsonRequestBehavior.AllowGet);
        }
        
        private void BindValues()
        {
            var multiplexes = _multiplexRepository.GetAll().OrderBy(m => m.Name);
            ViewBag.Multiplexes = _mappingEngine.Map<IList<MultiplexModel>>(multiplexes);
            ViewBag.Movies = _mappingEngine.Map<IList<MovieListModel>>(_movieRepository.GetAll());
            ViewBag.Screens = multiplexes.Select(m => new
            {
                MultiplexId = m.Id,
                Screens = m.Screens.Select(s => new { s.Id, s.Name })
            }).ToJson();
        }

    }
}
