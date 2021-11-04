using System;
using System.Collections.Generic;
using System.Text;

namespace Konso.Clients.Logging.Models.Requests
{
    public class CreateLogRequest
    {
        public CreateLogRequest() { }
        public long TimeStamp { get; set; }
        public List<string> Tags { get; set; }
        public string AppName { get; set; }
        public string CorrelationId { get; set; }
        public string MachineName { get; set; }
        public string Message { get; set; }
        public string Env { get; set; }
        public int? EventId { get; set; }
        public string Level { get; set; }

    }
}
