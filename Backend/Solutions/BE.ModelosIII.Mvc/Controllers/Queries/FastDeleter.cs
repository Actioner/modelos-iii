using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE.ModelosIII.Tasks.Commands.Run;
using BE.ModelosIII.Tasks.Commands.Scenario;
using SharpArch.NHibernate;

namespace BE.ModelosIII.Mvc.Controllers.Queries
{
    public class FastDeleter : NHibernateQuery, IFastDeleter
    {
        public void DeleteRun(DeleteRunCommand command) 
        {
            string queryString = @"delete from runs where runid = :runid;";

            Session
                .CreateSQLQuery(queryString)
                .SetParameter("runid", command.Id)
                .ExecuteUpdate();
        }

        public void DeleteScenario(DeleteScenarioCommand command) 
        {
            string queryString = @"delete from scenarios where scenarioid = :scenarioid;";

            Session
                .CreateSQLQuery(queryString)
                .SetParameter("scenarioid", command.Id)
                .ExecuteUpdate();
        }
    }
}