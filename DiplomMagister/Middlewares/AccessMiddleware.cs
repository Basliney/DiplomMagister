using static System.Net.Mime.MediaTypeNames;

namespace DiplomMagister.Middlewares
{
    public class AccessMiddleware
    {
        private readonly RequestDelegate _next;

        private List<string> ACCESS_PATHS = new List<string>()
        {"/account/logout", "/account/login", "/home/privacy"};

        public AccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // context.Request.Path = context.Request.Path.ToString().Replace("https", "http");

            if (!ACCESS_PATHS.Contains(context.Request.Path.ToString().ToLower()))
            {
                context.Request.Path = ("/Account/Login");
                context.Response.Redirect("/Account/Login");
            }
            await _next.Invoke(context);
            return;
        }
    }
}
