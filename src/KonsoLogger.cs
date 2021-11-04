using Konso.Clients.Logging.Extensions;
using Konso.Clients.Logging.Models;
using Konso.Clients.Logging.Models.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;

namespace Konso.Clients.Logging
{
    public class KonsoLogger : ILogger
    {
        private readonly string _name;
        private readonly KonsoLoggerConfig _loggerConfig;
        private readonly ILoggingClient _client;

        public KonsoLogger(string name, KonsoLoggerConfig _config, ILoggingClient client)
        {

            _name = name;
            _loggerConfig = _config;
            _client = client;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _loggerConfig.LogLevel;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            StringBuilder sb = new StringBuilder($"{_name} {logLevel.ToString()} - {eventId.Id}  - {formatter(state, exception)}");
            var correlationIdPart = state.
                ToString().
                Split(',').
                Where(x => x.ToLower().Contains("correlationid")).
                FirstOrDefault();

            var correlationId = string.IsNullOrWhiteSpace(correlationIdPart) ?
                null :
                correlationIdPart.
                    Split(' ').
                    Where(x => !x.ToLower().Contains("correlationid")).
                    FirstOrDefault();

            if (exception != null && !string.IsNullOrEmpty(exception.StackTrace))
            {
                sb.Append($", error: {exception.Message}, trace: {exception.StackTrace}");
            }

          
            var request = new CreateLogRequest()
            {
                MachineName = Environment.MachineName,
                AppName = _loggerConfig.AppName,
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
