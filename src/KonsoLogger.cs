using Konso.Clients.Logging.Extensions;
using Konso.Clients.Logging.Models;
using Konso.Clients.Logging.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;

namespace Konso.Clients.Logging
{
    public class KonsoLogger : ILogger
    {
        private readonly string _name;
        private readonly KonsoLoggerConfig _config;
        private readonly ILoggingClient _client;
        private readonly IHttpContextAccessor _accessor;

        public KonsoLogger(string name, IOptions<KonsoLoggerConfig> config, ILoggingClient client, IHttpContextAccessor accessor)
        {

            _name = name;
            _config = config.Value;
            _client = client;
            _accessor = accessor;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _config.LogLevel;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string correlationId = null;
            if (!IsEnabled(logLevel))
            {
                return;
            }

            StringBuilder sb = new StringBuilder($"{_name} {logLevel.ToString()} - {eventId.Id}  - {formatter(state, exception)}");

            if (_accessor != null && _accessor.HttpContext != null)
            {
                correlationId = _accessor.HttpContext.TraceIdentifier;
            }

            if (exception != null && !string.IsNullOrEmpty(exception.StackTrace))
            {
                sb.Append($", error: {exception.Message}, trace: {exception.StackTrace}");
            }

          
            var request = new CreateLogRequest()
            {
                MachineName = Environment.MachineName,
                AppName = _config.AppName,
                Message = sb.ToString(),
                Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                TimeStamp = DateTime.UtcNow.ToEpoch(),
                EventId = eventId.Id > 0 ? eventId.Id : new int?(),
                CorrelationId = correlationId,
                Level = logLevel.ToString()
            };


            await _client.CreateAsync(request);
        }
    }
}
