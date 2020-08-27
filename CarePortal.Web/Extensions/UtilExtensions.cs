using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;

namespace CarePortal.Web.Extensions
{
    public static class UtilExtensions
    {
        public static IHtmlContent LiActive(this IHtmlHelper htmlHelper, string controllerName, string activeClass)
        {
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            if (string.Compare(controllerName, currentController, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return new HtmlString(activeClass);
            }

            return new HtmlString(string.Empty);
        }

        public static string GetRequiredString(this RouteData routeData, string keyName)
        {
            object value;
            if (!routeData.Values.TryGetValue(keyName, out value))
            {
                throw new InvalidOperationException($"Could not find key with name '{keyName}'");
            }

            return value?.ToString();
        }

    }
}