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
            if (token != null && !string.IsNullOrEmpty(token.ToString()))
            {
                context.Request.Headers["Accept"] = "application/json";//.Add("Accept", "application/json");
                context.Request.Headers.Add("Authorization", "Bearer " + token);
                await _next.Invoke(context);
            }
            else
            {
                await new AccessMiddleware(_next).InvokeAsync(context);
            }
        }
    }
}
