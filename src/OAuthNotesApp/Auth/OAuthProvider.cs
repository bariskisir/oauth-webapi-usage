using Microsoft.Owin.Security.OAuth;
using OAuthNotesApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
namespace OAuthNotesApp.Auth
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            Uri expectedRootUri = new Uri(context.Request.Uri, "/");
            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                // Make user validation here
                var user = UserUtil.Login(context.UserName, context.Password);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

                    // Custom claims can be set from here
                    //identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));
                    //identity.AddClaim(new Claim(ClaimTypes.Gender, user.Gender));
                    //

                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            });
        }
    }
}