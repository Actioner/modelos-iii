using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;
using BE.ModelosIII.Tasks.Commands.Report;
using NHibernate.Transform;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public class MostSoldHourReportQuery : NHibernateQuery, IMostSoldHourReportQuery
    {
        public IList<MostSoldHourReportModel> GetReport(MostSoldCommand search)
        {
            string multiplexParam = search.MultiplexId.HasValue
                                        ? "and m.MultiplexId = " + search.MultiplexId.Value
                                        : string.Empty;

            string queryString = @"select CONCAT(DATEPART(hour, startTime),':',RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(minute, startTime)), 2)) as StartTime, sum(partialResult.seats) SoldTickets, count(partialResult.MovieId) MovieQuantity
                                        from (select cast(StartDate as time) as startTime, count(se.SeatId) as seats, mo.MovieId
	                                        from Screenings sc
		                                        inner join screens s on s.ScreenId = sc.ScreenId
		                                        inner join Multiplexes m on m.MultiplexId = s.MultiplexId
		                                        inner join Reservations r on r.ScreeningId = sc.ScreeningId
		                                        inner join ReservationsToSeats rs on rs.ReservationId = r.ReservationId
		                                        inner join seats se on se.SeatId = rs.SeatId
		                                        inner join Movies mo on sc.MovieId = mo.MovieId
	                                        where sc.StartDate >= :dateFrom and sc.StartDate < :dateTo and r.ReservationStatus != 'Cancel' and r.ReservationPaymentStatus = 'Paid' " + multiplexParam + @"
	                                        group by cast(StartDate as time), mo.MovieId) as partialResult
                                        group by startTime
                                        order by soldTickets desc, startTime;";

            var result = Session
                .CreateSQLQuery(queryString)
                .SetParameter("dateFrom", search.From.Date)
                .SetParameter("dateTo", search.To.AddDays(1).Date)
                .SetResultTransformer(Transformers.AliasToBean<MostSoldHourReportModel>())
                .List<MostSoldHourReportModel>();

            return result;
        }
    }
}