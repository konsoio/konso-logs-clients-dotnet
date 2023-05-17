using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Konso.Clients.Logging.Middleware
{
    public class KonsoRazorMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-Correlation-ID";

        public KonsoRazorMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            // get correlation id session
            if (!string.IsNullOrEmpty(context.Session.Id))
            {
                context.TraceIdentifier = context.Session.Id;
            }
            await _next(context);
        }
    }
}
