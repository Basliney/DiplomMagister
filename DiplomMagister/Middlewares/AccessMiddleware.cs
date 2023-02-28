using DiplomMagister.Data;
using DiplomMagister.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace DiplomMagister.Middlewares
{
    public class AccessMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            // context.Request.Path = context.Request.Path.ToString().Replace("https", "http");
            var token = context.Request.Headers["Authorization"].ToString();
            if (token is not null && token != string.Empty)
            {
                token = token.ToString().Replace("Bearer ", "");
                var secureToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                if (secureToken.ValidTo.CompareTo(DateTime.UtcNow) <= 0)
                {
                    try
                    {
                        var accountService = new AccountService(dbContext);
                        var userId = secureToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimsIdentity.DefaultNameClaimType))?.Value;
                        accountService.RenewTokenOfAuthorizedUser(context, userId);
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        await new TokenMiddleware(_next).InvokeAsync(context);
                    }
                }
                await _next.Invoke(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
