using Konso.Clients.Logging.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Konso.Clients.Logging.Extensions
{
    public static class KonsoLoggerBuilderExtensions
    {
        public static ILoggingBuilder AddKonsoLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggingClient, KonsoLoggingClient>();
            builder.Services.AddSingleton<ILoggerProvider, KonsoLoggerProvider>();
            return builder;
        }
    }
}
