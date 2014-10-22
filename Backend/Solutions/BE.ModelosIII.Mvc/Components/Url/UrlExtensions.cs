using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.ModelosIII.Infrastructure.Helpers;

namespace BE.ModelosIII.Mvc.Components.Url
{
    public static class UrlExtensions
    {
        public static string OwnerAction(this UrlHelper url, string action, string controller, object query)
        {
            return string.Format("/Owner/{0}/{1}{2}", controller, action, query != null ? query.ToQueryString() : null);
        }

        public static string OwnerAction(this UrlHelper url, string action, string controller, uint id)
        {
            return string.Format("/Owner/{0}/{1}/{2}", controller, action, id);
        }
    }
}