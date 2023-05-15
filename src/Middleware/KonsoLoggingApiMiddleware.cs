using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Konso.Clients.Logging.Middleware
{
    public class KonsoLoggingApiMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-Correlation-ID";

        public KonsoLoggingApiMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            // get correlation id from header 
            if (context.Request.Headers.TryGetValue(HeaderName, out StringValues correlationId))
            {
                context.TraceIdentifier = correlationId;
            }
            await _next(context);
        }
    }
}
