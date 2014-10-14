using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.Helpers;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Mvc.Models.Multiplex;
using BE.ModelosIII.Mvc.Models.Reservation;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMultiplexRepository _multiplexRepository;
        private readonly IScreeningRepository _screeningRepository;
        private readonly IMappingEngine _mappingEngine;

        public ReservationController(
            IReservationRepository reservationRepository,
            IMultiplexRepository multiplexRepository,
            IScreeningRepository screeningRepository,
            IMappingEngine mappingEngine)
        {

            _reservationRepository = reservationRepository;
            _multiplexRepository = multiplexRepository;
            _screeningRepository = screeningRepository;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Index/
        public ActionResult Index(ReservationSearchModel searchModel)
        {
            var screening = _screeningRepository.Get(searchModel.ScreeningId);
            var reservationListModel = new ReservationListModel
            {
                ReservationSearch = searchModel
            };

            BindIndexValues();

            if (screening == null)
            {
                return View(reservationListModel);
            }

            var reservations = _reservationRepository.GetByScreening(screening);
            
            var reservationModels = _mappingEngine.Map<IList<ReservationModel>>(reservations
                .OrderByDescending(sc => sc.Time)
                .ThenBy(sc => sc.User.Name));

            reservationListModel.Reservations = reservationModels;

            return View(reservationListModel);
        }

        private void BindIndexValues()
        {
            var multiplexes = _multiplexRepository.GetAll().OrderBy(m => m.Name);
            ViewBag.Multiplexes = _mappingEngine.Map<IList<MultiplexModel>>(multiplexes);
            ViewBag.Screens = multiplexes.Select(m => new
                                                     {
                                                         MultiplexId = m.Id,
                                                         Screens = m.Screens.Select(s => new { s.Id, s.Name })
                                                     }).ToJson();
        }
    }
}
