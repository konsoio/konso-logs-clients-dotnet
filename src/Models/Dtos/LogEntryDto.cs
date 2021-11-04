using System.Collections.Generic;

namespace Konso.Clients.Logging.Models.Dtos
{
    public class LogEntryDto
    {
        public string MachineName
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public string Env
        {
            get;
            set;
        }

        public int? ResultCode
        {
            get;
            set;
        }

        public int? EventId
        {
            get;
            set;
        }

        public string BucketId
        {
            get;
            set;
        }

        public string Level
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public long TimeStamp
        {
            get;
            set;
        }

        public string AppName
        {
            get;
            set;
        }

        public List<string> Tags
        {
            get;
            set;
        }

        public string CorrelationId
        {
            get;
            set;
        }
    }
}
