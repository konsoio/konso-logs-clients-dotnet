using Konso.Clients.Logging.Models.Dtos;
using Konso.Clients.Logging.Models.Requests;
using System.Threading.Tasks;

namespace Konso.Clients.Logging.Models
{
    public interface ILoggingClient
    {
        Task<bool> CreateAsync(CreateLogRequest request);

        Task<PagedResponse<LogEntryDto>> GetByAsync(GetLogsRequest request);
    }
}
