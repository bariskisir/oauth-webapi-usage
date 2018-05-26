using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OAuthNotesApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Cors is allowed for any domain.
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            //

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
