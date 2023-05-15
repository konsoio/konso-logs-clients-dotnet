using Konso.Clients.Logging.Models;
using Konso.Clients.Logging.Models.Dtos;
using Konso.Clients.Logging.Models.Requests;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Konso.Clients.Logging
{
    public class KonsoLoggingClient : ILoggingClient, IDisposable
    {
        private readonly HttpClient _client;
        private readonly KonsoLoggerConfig _config;
        public KonsoLoggingClient(KonsoLoggerConfig config)
        { 
            _client = new HttpClient();
            _config = config;
        }
        public async Task<bool> CreateAsync(CreateLogRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(_config.Endpoint)) throw new Exception("Endpoint is not defined");
                if (string.IsNullOrEmpty(_config.BucketId)) throw new Exception("Bucket is not defined");
                if (string.IsNullOrEmpty(_config.ApiKey)) throw new Exception("API key is not defined");

                // serialize request as json
                var jsonStr = JsonSerializer.Serialize(request);

                if (!_client.DefaultRequestHeaders.Contains("x-api-key"))
                {
                    if (!_client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", _config.ApiKey)) throw new Exception("Missing API key");
                }

                var httpItem = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_config.Endpoint}/v1/logs/{_config.BucketId}", httpItem);
                if (!response.IsSuccessStatusCode)
                    throw new Exception(string.Format("Error sending log status code {0}", response.StatusCode));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        public async Task<KonsoPagedResponse<LogEntryDto>> GetByAsync(GetLogsRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(_config.Endpoint)) throw new Exception("Endpoint is not defined");
                if (string.IsNullOrEmpty(_config.BucketId)) throw new Exception("Bucket is not defined");
                if (string.IsNullOrEmpty(_config.ApiKey)) throw new Exception("API key is not defined");

                if(!_client.DefaultRequestHeaders.Contains("x-api-key"))
                {
                    if (!_client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", _config.ApiKey)) throw new Exception("Missing API key");
                }
                    
                int sortNum = (int)request.Sort;
                var builder = new UriBuilder($"{_config.Endpoint}/v1/logs/{_config.BucketId}")
                {
                    Port = -1
                };
                var query = HttpUtility.ParseQueryString(builder.Query);
                if (!string.IsNullOrEmpty(request.AppName))
                    query["appName"] = request.AppName;

                if (!string.IsNullOrEmpty(request.CorrelationId))
                {
                    query["correlationId"] = request.CorrelationId;
                }
                if (!string.IsNullOrEmpty(request.Message))
                {
                    query["message"] = request.Message;
                }

                if (!string.IsNullOrEmpty(request.Level))
                    query["level"] = request.Level;

                if (request.DateFrom.HasValue)
                    query["fromDate"] = request.DateFrom.ToString();

                if (request.DateTo.HasValue)
                    query["toDate"] = request.DateTo.ToString();
                if (sortNum > 0)
                    query["sort"] = sortNum.ToString();
                query["from"] = request.From.ToString();
                query["to"] = request.To.ToString();

                query["correlationId"] = request.CorrelationId;
                if (request.Tags != null && request.Tags.Count > 0)
                {
                    query["tags"] = String.Join(",", request.Tags);
                }

                builder.Query = query.ToString();
                string url = builder.ToString();

                string responseBody = await _client.GetStringAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var responseObj = JsonSerializer.Deserialize<KonsoPagedResponse<LogEntryDto>>(responseBody, options);
                return responseObj;
            }
            catch
            {
                throw;
            }
        }
    }
}
