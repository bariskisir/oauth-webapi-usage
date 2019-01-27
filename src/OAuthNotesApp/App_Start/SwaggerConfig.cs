using OAuthNotesApp.App_Start;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "RegisterSwaggerConfig")]
namespace OAuthNotesApp.App_Start
{
    public class SwaggerConfig
    {
        public static void RegisterSwaggerConfig()
        {
            GlobalConfiguration.Configuration.EnableSwagger(x => x.SingleApiVersion("v1", "NotesAPI")).EnableSwaggerUi();
        }
    }
}