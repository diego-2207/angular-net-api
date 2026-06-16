using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using EspecificacionesTecnicas.Api.DA;
namespace EspecificacionesTecnicas.Api.Utils
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public UserDB _userService { get; set; }
        public BasicAuthenticationHandler(
         IOptionsMonitor<AuthenticationSchemeOptions> options,
         ILoggerFactory logger,
         UrlEncoder encoder) : base(options, logger, encoder)
        {
            _userService = new UserDB();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                if (IsValidUser(username, password))
                {
                    string rolUsuario = _userService.getUserRol(username);
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, rolUsuario)
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }

                return AuthenticateResult.Fail("Usuario o Clave incorrectos");
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }

        private bool IsValidUser(string username, string password)
        {
            string storedPassword = _userService.getUserPassword(username);
            if (storedPassword == null) return false;
            // Verify toma la clave plana, le aplica el salt que está dentro de storedPassword 
            // y comprueba si coinciden.
            return BCrypt.Net.BCrypt.Verify(password, storedPassword);
        }
    }
}
