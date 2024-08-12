using Microsoft.AspNetCore.Http;

namespace ReadRealmBackend.Common.Services
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
                var userId = context.User.FindFirst("sub")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    context.Items["UserId"] = userId;
                }
            }

            await _next(context);
        }
    }
}