using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;
using SFA.DAS.CollectionCalendar.InnerAPI;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests
{
    public class TestWebApi : WebApplicationFactory<Startup>
    {
        private readonly TestContext _context;
        private readonly Dictionary<string, string> _config;

        public TestWebApi(TestContext context)
        {
            _context = context;

            _config = new Dictionary<string, string>{
                    { "EnvironmentName", "LOCAL_ACCEPTANCE_TESTS" },
                    { "ConfigurationStorageConnectionString", "UseDevelopmentStorage=true" },
                    { "ApplicationSettings:DbConnectionString", _context.SqlDatabase.DatabaseInfo.ConnectionString },
                    { "ConfigNames", "SFA.DAS.CollectionCalendar" }
                };
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(a =>
            {
                a.Sources.Clear();
                a.AddInMemoryCollection(_config);
            });
            builder.ConfigureServices(s =>
            {
                s.Configure<ApplicationSettings>(a =>
                {
                    a.DbConnectionString = _context.ApplicationSettings.DbConnectionString;
                });
            });
            builder.UseEnvironment("Development");
        }
    }
}
