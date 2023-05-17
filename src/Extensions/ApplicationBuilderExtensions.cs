using Konso.Clients.Logging.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Konso.Clients.Logging.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHttpLogTracing(this IApplicationBuilder app)
        {
            app.UseMiddleware<KonsoLoggingApiMiddleware>();
        
            return app;
        }


        public static IApplicationBuilder UseSessionLogTracing(this IApplicationBuilder app)
        {
            app.UseMiddleware<KonsoRazorMiddleware>();

            return app;
        }
    }
}
