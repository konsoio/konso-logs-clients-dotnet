using Microsoft.Extensions.Logging;

namespace Konso.Clients.Logging.Models
{
    public class KonsoLoggerConfig
    {
        public LogLevel LogLevel { get; set; }
        public string AppName { get; set; }
        public string BucketId { get; set; }
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
    }
}
