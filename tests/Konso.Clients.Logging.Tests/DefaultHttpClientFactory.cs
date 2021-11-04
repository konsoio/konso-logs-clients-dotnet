using System.Net.Http;

namespace Konso.Clients.Logging.Tests
{
    public sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => new HttpClient();
    }
}
