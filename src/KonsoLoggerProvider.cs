using Konso.Clients.Logging.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Konso.Clients.Logging
{
    public class KonsoLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, KonsoLogger> _loggers = new();
        private readonly ILoggingClient _client;
        private KonsoLoggerConfig _currentConfig;
        public KonsoLoggerProvider(KonsoLoggerConfig konsoLogger, ILoggingClient client)
        {
            _currentConfig = konsoLogger;
            _client = client;
        }
        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new KonsoLogger(name, GetCurrentConfig(), _client));

        private KonsoLoggerConfig GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
