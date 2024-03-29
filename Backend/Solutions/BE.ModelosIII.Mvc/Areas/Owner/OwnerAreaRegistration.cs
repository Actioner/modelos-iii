﻿using System.Web.Mvc;

namespace BE.ModelosIII.Mvc.Areas.Owner
{
    public class OwnerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Owner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Owner_default",
                "Owner/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
