using System.Collections.Generic;

namespace Konso.Clients.Logging.Models
{
    public class KonsoPagedResponse<T>
    {
        /// <summary>
        /// List of entities
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// Total amount of records
        /// </summary>
        public long Total { get; set; }
    }
}
