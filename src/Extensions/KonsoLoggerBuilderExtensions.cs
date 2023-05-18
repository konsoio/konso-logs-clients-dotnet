using Konso.Clients.Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Konso.Clients.Logging.Extensions
{
    public static class KonsoLoggerBuilderExtensions
    {
        public static ILoggingBuilder AddKonsoLogger(this ILoggingBuilder builder, Action<KonsoLoggerConfig> configureOptions)
        {
            builder.Services.AddOptions();

            builder.Services.AddSingleton<ILoggingClient, KonsoLoggingClient>();
            builder.Services.AddSingleton<ILoggerProvider, KonsoLoggerProvider>();
            // setup configuration
            builder.Services.Configure<KonsoLoggerConfig>(configureOptions);

            return builder;
        }
    }
}
