using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.CollectionCalendar.DataAccess;
using SFA.DAS.CollectionCalendar.Infrastructure;
using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;
using SFA.DAS.CollectionCalendar.Queries;
using SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authentication;
using SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authorization;

namespace SFA.DAS.CollectionCalendar.InnerApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationInsightsTelemetry();

            if (!ConfigurationIsAcceptanceTests(builder.Configuration))
            {
                builder.Configuration.AddAzureTableStorage(options =>
                {
                    options.ConfigurationKeys = new[] {"SFA.DAS.CollectionCalendar"};
                    options.StorageConnectionString = builder.Configuration["ConfigurationStorageConnectionString"];
                    options.EnvironmentName = builder.Configuration["EnvironmentName"];
                    options.PreFixConfigurationKeys = false;
                });
            }

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Collection Calendar Internal API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            var applicationSettings = new ApplicationSettings();
            builder.Configuration.Bind(nameof(ApplicationSettings), applicationSettings);
            builder.Services.AddDbContext<CollectionCalendarDataContext>();
            builder.Services.AddEntityFramework(applicationSettings, NotLocal(builder.Configuration));
            builder.Services.AddSingleton(x => applicationSettings);
            builder.Services.AddQueryServices();
            builder.Services.AddHealthChecks();
            builder.Services.AddApiAuthentication(applicationSettings, builder.Environment.IsDevelopment());
            builder.Services.AddApiAuthorization(builder.Environment.IsDevelopment());
            var app = builder.Build();

            app.MapHealthChecks("/ping");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            static bool NotLocal(IConfiguration configuration)
            {
                return !configuration!["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        private static bool ConfigurationIsAcceptanceTests(IConfiguration configuration)
        {
            return configuration["EnvironmentName"].Equals("LOCAL_ACCEPTANCE_TESTS", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}