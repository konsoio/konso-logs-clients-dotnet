using FluentAssertions;
using Konso.Clients.Logging.Extensions;
using Konso.Clients.Logging.Models;
using Konso.Clients.Logging.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Konso.Clients.Logging.Tests
{
    public class KonsoLoggerTests 
    {
        private const string apiUrl = "https://apis.konso.io";
        private const string bucketId = "<your bucket id>";
        private const string apiKey = "<your access key>";
        private const string app = "TestApp";
       

        public KonsoLoggerTests()
        {

        }
       
        [Fact]
        public async Task Create_SimpleLog()
        {
            // arrange
            var service = new KonsoLoggingClient(new KonsoLoggerConfig()
            {
                ApiKey = apiKey,
                AppName = app,
                BucketId = bucketId,
                Endpoint = apiUrl
            });

            // act
            var o = new CreateLogRequest() { AppName = "test", Message = "We will log this message", TimeStamp = DateTime.UtcNow.ToEpoch(), Tags = new List<string>() { "test" }, Level = "info" };
            var res = await service.CreateAsync(o);

            // assert
            res.Should().BeTrue();

        }

        [Fact]
        public async Task CreateAndGet_SimpleLog()
        {
            // arrange
            var service = new KonsoLoggingClient(new KonsoLoggerConfig()
            {
                ApiKey = apiKey,
                AppName = app,
                BucketId = bucketId,
                Endpoint = apiUrl
            });

            var o = new CreateLogRequest() { AppName = "test", Message = "We will log this message", TimeStamp = DateTime.UtcNow.ToEpoch(), Tags = new List<string>() { "test" }, Level = "info" };
            var res = await service.CreateAsync(o);

            // act
            var pagedList = await service.GetByAsync(new GetLogsRequest() { From = 0, To = 10 });

            // assert
            pagedList.Should().NotBeNull();
            pagedList.Total.Should().BeGreaterThan(0);

        }
    }
}
