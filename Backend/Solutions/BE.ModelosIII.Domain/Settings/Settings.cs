using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BE.ModelosIII.Domain.Settings
{
    public static class BackendSettings
    {
        public static int ScreeningIntervalInMinutes
        {
            get
            {
                string movieListingsSpan = ConfigurationManager.AppSettings["ScreeningIntervalInMinutes"];

                return movieListingsSpan != null ? Convert.ToInt32(movieListingsSpan) : 20;
            }
        }
    }
}
