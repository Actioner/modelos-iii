using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure.ApplicationServices;
using BE.ModelosIII.Mvc.Controllers;
using System.Web.Mvc;
using BE.ModelosIII.Mvc.Controllers.Queries;
using BE.ModelosIII.Mvc.Models.Multiplex;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Mvc.Models.Util;
using BE.ModelosIII.Resources;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IMostSoldHourReportQuery _mostSoldHourReportQuery;
        private readonly IMostSoldMovieReportQuery _mostSoldMovieReportQuery;
        private readonly IMultiplexRepository _multiplexRepository;
        private readonly IPdfManager _pdfManager;
        private readonly IMappingEngine _mappingEngine;

        public ReportController(
            IMostSoldHourReportQuery mostSoldHourReportQuery,
            IMostSoldMovieReportQuery mostSoldMovieReportQuery,
            IMultiplexRepository multiplexRepository,
            IPdfManager pdfManager,
            IMappingEngine mappingEngine)
        {
            _mostSoldHourReportQuery = mostSoldHourReportQuery;
            _mostSoldMovieReportQuery = mostSoldMovieReportQuery;
            _multiplexRepository = multiplexRepository;
            _pdfManager = pdfManager;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /MostSoldHour/
        [HttpGet]
        public ActionResult MostSoldHour()
        {
            BindSearchValues();
            return View("Hour/Index", new MostSoldHourListModel());
        }

        //
        // POST: /MostSoldHour/
        [HttpPost]
        public ActionResult MostSoldHour(MostSoldCommand search)
        {
            BindSearchValues();
            var result = new MostSoldHourListModel
                             {
                                 Search = search
                             };

            if (ModelState.IsValid)
            {
                result.ReportRows = _mostSoldHourReportQuery.GetReport(search);
                var action = new ActionModel("Exportar", "MostSoldHourExport", IconType.Create)
                                 {
                                     RouteValues = new
                                                       {
                                                           from = search.From,
                                                           to = search.To,
                                                           multiplexid = search.MultiplexId
                                                       }
                                 };
                ViewBag.Actions = new List<ActionModel>
                                  {
                                      action,
                                  };
            }

            return View("Hour/Index", result);
        }

        //
        // POST: /MostSoldHour/
        [HttpGet]
        public FileContentResult MostSoldHourExport(MostSoldCommand search)
        {
            var reportRows = _mostSoldHourReportQuery.GetReport(search);
            string multiplex = search.MultiplexId.HasValue ? _multiplexRepository.Get(search.MultiplexId.Value).Name : string.Empty;

            var reportInfo = new Infrastructure.ApplicationServices.Models.MostSoldHour.ReportInfo
                                 {
                                     Title = ReportMessages.MostSoldHourTitle,
                                     From = search.From.ToString("dd/MM/yyyy"),
                                     To = search.To.ToString("dd/MM/yyyy"),
                                     Multiplex = multiplex,
                                     Items = _mappingEngine.Map<IList<Infrastructure.ApplicationServices.Models.MostSoldHour.ReportDataItem>>(reportRows)
                                 };

            var fileName = string.Format("{0}_{1}.pdf", ReportMessages.MostSoldHourTitle, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var pdfData = _pdfManager.GetMostSoldHourContent(reportInfo);

            return File(pdfData, "application/pdf", fileName);
        }


        //
        // GET: /MostSoldMovie/
        [HttpGet]
        public ActionResult MostSoldMovie()
        {
            BindSearchValues();
            return View("Movie/Index", new MostSoldMovieListModel());
        }

        //
        // POST: /MostSoldMovie/
        [HttpPost]
        public ActionResult MostSoldMovie(MostSoldCommand search)
        {
            BindSearchValues();
            var result = new MostSoldMovieListModel
            {
                Search = search
            };

            if (ModelState.IsValid)
            {
                result.ReportRows = _mostSoldMovieReportQuery.GetReport(search);
                var action = new ActionModel("Exportar", "MostSoldMovieExport", IconType.Create)
                {
                    RouteValues = new
                    {
                        from = search.From,
                        to = search.To,
                        multiplexid = search.MultiplexId
                    }
                };
                ViewBag.Actions = new List<ActionModel>
                                  {
                                      action,
                                  };
            }

            return View("Movie/Index", result);
        }

        //
        // POST: /MostSoldMovie/
        [HttpGet]
        public FileContentResult MostSoldMovieExport(MostSoldCommand search)
        {
            var reportRows = _mostSoldMovieReportQuery.GetReport(search);
            string multiplex = search.MultiplexId.HasValue ? _multiplexRepository.Get(search.MultiplexId.Value).Name : string.Empty;

            var reportInfo = new Infrastructure.ApplicationServices.Models.MostSoldMovie.ReportInfo
            {
                Title = ReportMessages.MostSoldMovieTitle,
                From = search.From.ToString("dd/MM/yyyy"),
                To = search.To.ToString("dd/MM/yyyy"),
                Multiplex = multiplex,
                Items = _mappingEngine.Map<IList<Infrastructure.ApplicationServices.Models.MostSoldMovie.ReportDataItem>>(reportRows)
            };

            var fileName = string.Format("{0}_{1}.pdf", ReportMessages.MostSoldMovieTitle, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var pdfData = _pdfManager.GetMostSoldMovieContent(reportInfo);

            return File(pdfData, "application/pdf", fileName);
        }

        private void BindSearchValues()
        {
            var multiplexes = _multiplexRepository.GetAll().OrderBy(m => m.Name);
            ViewBag.Multiplexes = _mappingEngine.Map<IList<MultiplexModel>>(multiplexes);
        }
    }
}