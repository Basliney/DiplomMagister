namespace DiplomMagister.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["accessToken"];//.FirstOrDefault(x=>x.Key.Equals("accessToken")).Value;
            if (token != null && !string.IsNullOrEmpty(token.ToString()) && string.IsNullOrEmpty(context.Request.Headers["Authorization"].ToString()))
            {
                context.Request.Headers["Accept"] = "application/json";//.Add("Accept", "application/json");
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            else
            {
                List<string> ACCESS_PATHS = new List<string>()
                    {"/account/login", "/account/register", "/home/privacy"};

                if (!ACCESS_PATHS.Contains(context.Request.Path.ToString().ToLower()))
                {
                    context.Request.Path = ("/Account/Login");
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
