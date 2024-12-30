using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Auth.Handler
{
    public class BasicAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions> {
        private string _failReason="";
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock):base(options, logger, encoder, clock)
        {

        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
            {
                this._failReason = "No Header found";
                return Task.FromResult(AuthenticateResult.Fail(_failReason));
            }

            var _headervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(_headervalue.Parameter);
            string credentials = Encoding.UTF8.GetString(bytes);
            if(!String.IsNullOrEmpty(credentials))
            {
                string[] arr = credentials.Split(":");
                string username = arr[0];
                string pass = arr[1];

                if (username != "adminuser" || pass != "admin") {
                    return Task.FromResult(AuthenticateResult.Fail("UnAuthorized"));
                }

                var claim = new[]{new Claim(ClaimTypes.Name, username)};
                var identity = new ClaimsIdentity(claim, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            } else {
                return Task.FromResult(AuthenticateResult.Fail("UnAuthorized"));
            }
        }
    }
}
