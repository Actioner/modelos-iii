using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE.ModelosIII.Infrastructure.Helpers
{
    public static class UrlExtensionHelper
    {
        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            var values = properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString())))
                .ToList();

            string firstProperty = "?";

            if (properties.Any())
            {
                firstProperty += values.First();
            }

            // Concat all key/value pairs into a string separated by ampersand
            return firstProperty + string.Join("&", values.Skip(1));
        }


        public static string AbsoluteAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues)
        {
            return urlHelper.Action(actionName, controllerName, routeValues, HttpContext.Current.Request.Url.Scheme);
        }
    }
}
