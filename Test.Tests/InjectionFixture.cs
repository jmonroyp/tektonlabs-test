using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Test.Tests
{
    public class InjectionFixture : IDisposable
    {
        public readonly TestServer server;
        public readonly HttpClient client;

        public InjectionFixture()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        public IServiceProvider ServiceProvider => server.Host.Services;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                server.Dispose();
                client.Dispose();
            }
        }
    }
}