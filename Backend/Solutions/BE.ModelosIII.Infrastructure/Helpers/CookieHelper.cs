using System;
using System.Linq;
using System.Web;

namespace BE.ModelosIII.Infrastructure.Helpers
{
    public class CookieHelper
    {
        public static HttpCookie ObtainCookie(HttpContextBase context, string key)
        {
            HttpCookie cookie = null;
            if (context.Request.Cookies.AllKeys.Any(x => x == key))
            {
                cookie = context.Request.Cookies.Get(key);
                cookie.Expires = DateTime.Now.AddDays(1);
                context.Response.Cookies.Add(cookie);
            }
            return cookie;
        }

        public static HttpCookie GenerateCookie(HttpContextBase context, string key)
        {
            var cookie = new HttpCookie(key)
            {
                Expires = DateTime.Now.AddDays(1)
            };
            context.Response.Cookies.Add(cookie);
            return cookie;
        }

        public static void RemoveCookie(HttpContextBase context, string key)
        {
            if (context.Request.Cookies.AllKeys.Any(x => x == key))
            {
                var cookie = context.Request.Cookies.Get(key);
                cookie.Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies.Add(cookie);
            }
        }

        public static string GetCookieValue(HttpContextBase context, string cookieName, string key)
        {
            var cookie = ObtainCookie(context, cookieName);

            if (cookie != null && cookie.HasKeys)
            {
                var value = cookie[key];
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }
            return string.Empty;
        }
    }
}
