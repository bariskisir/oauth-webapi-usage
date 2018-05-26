using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(OAuthNotesApp.Auth.Startup))]

namespace OAuthNotesApp.Auth
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(appBuilder);
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                // Set url for token requests
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
                // HTTPS is highly recommended!
                AllowInsecureHttp = true,
                //
                Provider = new OAuthProvider()
            };
            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}