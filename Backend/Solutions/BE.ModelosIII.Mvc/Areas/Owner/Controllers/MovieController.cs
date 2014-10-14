using System.Web.Routing;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Models.Movie;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Tasks.Commands.Movie;
using SharpArch.Domain.Commands;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Web.Mvc;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class MovieController : BaseController
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IMovieRepository _movieRepository;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IMappingEngine _mappingEngine;

        public MovieController(
            ICommandProcessor commandProcessor,
            IMovieRepository movieRepository,
            IRepository<Rating> ratingRepository,
            IMappingEngine mappingEngine)
        {

            _commandProcessor = commandProcessor;
            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Index/
        public ActionResult Index()
        {
            var movies = _movieRepository.GetAll();
            var movieModels = _mappingEngine.Map<IList<MovieModel>>(movies);

            ViewBag.Actions = new List<ActionModel>
                                  {
                                      new ActionModel("Crear", "Create", IconType.Create),
                                  };

            return View(movieModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BindValues();

            return View(new CreateMovieCommand());
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMovieCommand command)
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
            var movie = _movieRepository.Get(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            BindValues();
            var command = _mappingEngine.Map<Movie, EditMovieCommand>(movie);

            return View(command);
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMovieCommand command)
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
        [Transaction]
        [ValidateInput(false)]
        public ActionResult Delete(DeleteMovieCommand command)
        {
            _commandProcessor.Process(command);
            return RedirectToAction("Index");
        }

        private void BindValues()
        {
            ViewBag.Ratings = _mappingEngine.Map<IList<Rating>, IList<RatingModel>>(_ratingRepository.GetAll());
        }
    }
}
