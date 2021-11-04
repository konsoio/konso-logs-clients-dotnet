using Konso.Clients.Logging.Models.Requests.Enums;
using System.Collections.Generic;

namespace Konso.Clients.Logging.Models.Requests
{
    public class GetLogsRequest
    {
        public string BucketId { get; set; }
        public string AppName { get; set; }
        public string Env { get; set; }
        public string MachineName { get; set; }
        public string Level { get; set; }
        public int? EventId { get; set; }
        public long? DateFrom { get; set; }
        public long? DateTo { get; set; }
        public int From { get; set; } = 0;
        public int To { get; set; } = 10;
        public SortingTypes Sort { get; set; } = SortingTypes.TimeStampDesc;
        public string Message { get; set; }

        public string Id { get; set; }
        public List<string> Tags { get; set; }

        public string CorrelationId { get; set; }
    }
}
