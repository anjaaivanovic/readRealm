namespace ReadRealmBackend.API.Middleware
{
    public class JwtHelper
    {
        private readonly RequestDelegate _next;

        public JwtHelper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.Identity.Name;

                if (!string.IsNullOrEmpty(userId))
                {
                    context.Items["userId"] = userId;
                }
            }

            await _next(context);
        }
    }
}