using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BE.ModelosIII.Infrastructure
{
    public static class BackendSettings
    {
        public static IList<string> AllowedRoles
        {
            get
            {
                string allowedRoles = ConfigurationManager.AppSettings["AllowedRoles"];

                return string.IsNullOrWhiteSpace(allowedRoles) ? null : allowedRoles.Split(',').Select(r => r.Trim(' ')).ToList();
            }
        }
    }
}
