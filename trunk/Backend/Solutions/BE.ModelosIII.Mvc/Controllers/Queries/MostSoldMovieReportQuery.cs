using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Tasks.Commands.Report;
using NHibernate.Transform;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public class MostSoldMovieReportQuery : NHibernateQuery, IMostSoldMovieReportQuery
    {
        public IList<MostSoldMovieReportModel> GetReport(MostSoldCommand search)
        {
            string multiplexParam = search.MultiplexId.HasValue
                                        ? "and m.MultiplexId = " + search.MultiplexId.Value
                                        : string.Empty;

            string queryString = @"select count(se.SeatId) as SoldTickets, mo.Title as Movie, m.Name as Multiplex
                                from Screenings sc
	                                inner join screens s on s.ScreenId = sc.ScreenId
	                                inner join Multiplexes m on m.MultiplexId = s.MultiplexId
	                                inner join Reservations r on r.ScreeningId = sc.ScreeningId
	                                inner join ReservationsToSeats rs on rs.ReservationId = r.ReservationId
	                                inner join seats se on se.SeatId = rs.SeatId
	                                inner join Movies mo on sc.MovieId = mo.MovieId
                                where sc.StartDate >= :dateFrom
	                                and sc.StartDate < :dateTo 
	                                and r.ReservationStatus != 'Cancel' and r.ReservationPaymentStatus = 'Paid'
                                    " + multiplexParam + @"
                                group by mo.Title, m.Name
                                order by SoldTickets desc";

            var result = Session
                .CreateSQLQuery(queryString)
                .SetParameter("dateFrom", search.From.Date)
                .SetParameter("dateTo", search.To.AddDays(1).Date)
                .SetResultTransformer(Transformers.AliasToBean<MostSoldMovieReportModel>())
                .List<MostSoldMovieReportModel>();

            return result;
        }
    }
}