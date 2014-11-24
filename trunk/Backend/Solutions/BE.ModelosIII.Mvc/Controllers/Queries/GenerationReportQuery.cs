using System.Collections.Generic;
using BE.ModelosIII.Mvc.Models.Report;
using NHibernate.Transform;
using SharpArch.NHibernate;
using BE.ModelosIII.Mvc.Models.Run;
using BE.ModelosIII.Tasks.Commands.Report;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public class GenerationReportQuery : NHibernateQuery, IGenerationReportQuery
    {
        public IList<GenerationReportModel> GetReport(GenerationReportSearchCommand search)
        {
            string scenarioParam = search.ScenarioId > 0
                                        ? "and m.ScenarioId = " + search.ScenarioId
                                        : string.Empty;

            string queryString = @"Select r.RunId, g.Number, ps.Best, ps.Average, ps.Worst
                                    from runs r
	                                    left join Generations g on g.RunId = r.RunId
	                                    left join (select p.GenerationId, max(p.Fitness) Best, avg(p.Fitness) Average, min(p.Fitness) Worst
			                                    from populations p
			                                    group by p.GenerationId) as ps on ps.GenerationId = g.GenerationId
                                    where r.runid = :runId
                                    order by g.Number;";

            var result = Session
                .CreateSQLQuery(queryString)
                .SetParameter("runId", search.RunId)
                .SetResultTransformer(Transformers.AliasToBean<GenerationReportModel>())
                .List<GenerationReportModel>();

            return result;
        }
    }
}