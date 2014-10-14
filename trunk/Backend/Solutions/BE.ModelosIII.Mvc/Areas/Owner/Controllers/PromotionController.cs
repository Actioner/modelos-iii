using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Models.Multiplex;
using BE.ModelosIII.Mvc.Models.Promotion;
using System.Collections.Generic;
using System.Web.Mvc;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Tasks.Commands.Promotion;
using SharpArch.Domain.Commands;
using SharpArch.NHibernate.Web.Mvc;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class PromotionController : BaseController
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IMappingEngine _mappingEngine;
        private readonly ICommandProcessor _commandProcessor;

        public PromotionController(
            IPromotionRepository promotionRepository,
            IPriceRepository priceRepository,
            ICommandProcessor commandProcessor,
            IMappingEngine mappingEngine)
        {

            _promotionRepository = promotionRepository;
            _priceRepository = priceRepository;
            _commandProcessor = commandProcessor;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Index/
        public ActionResult Index()
        {
            var promotions = _promotionRepository.GetAll()
                .OrderByDescending(p => p.EndDate)
                .ThenByDescending(p => p.Active);
            var price = _priceRepository.GetGeneralPrice();

            ViewBag.Actions = new List<ActionModel>
                                  {
                                      new ActionModel("Crear", "Create", IconType.Create),
                                  };

            var model = new PricePromotiontModel
                            {
                                GeneralPrice = new GeneralPriceCommand
                                                   {
                                                       Value = price.Value
                                                   },
                                Promotions = _mappingEngine.Map<IList<PromotionModel>>(promotions)
                            };

            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            BindValues();

            return View(new CreatePromotionCommand());
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePromotionCommand command)
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
            var movie = _promotionRepository.Get(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            BindValues();
            var command = _mappingEngine.Map<Promotion, EditPromotionCommand>(movie);

            return View(command);
        }


        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPromotionCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("Index");
            }
            BindValues();

            return View(command);
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult GeneralPrice(GeneralPriceCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
                return RedirectToAction("Index");
            }

            var promotions = _promotionRepository.GetAll()
                .OrderByDescending(p => p.EndDate)
                .ThenByDescending(p => p.Active);
            ViewBag.Actions = new List<ActionModel>
                                  {
                                      new ActionModel("Crear", "Create", IconType.Create),
                                  };

            var model = new PricePromotiontModel
            {
                GeneralPrice = command,
                Promotions = _mappingEngine.Map<IList<PromotionModel>>(promotions)
            };

            return View("Index", model);
        }

        [HttpGet]
        [Transaction]
        [ValidateInput(false)]
        public ActionResult Delete(DeletePromotionCommand command)
        {
            _commandProcessor.Process(command);
            return RedirectToAction("Index");
        }


        private void BindValues()
        {
        }
    }
}
