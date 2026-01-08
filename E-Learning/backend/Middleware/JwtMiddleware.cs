using backend.Helpers;

namespace backend.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtHelper _jwtHelper;

        public JwtMiddleware(RequestDelegate next, IJwtHelper jwtHelper)
        {
            _next = next;
            _jwtHelper = jwtHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var userId = _jwtHelper.ValidateToken(token);
                if (userId != null)
                {
                    // Attach user to context on successful jwt validation
                    context.Items["User"] = userId;
                }
            }

            await _next(context);
        }
    }
}