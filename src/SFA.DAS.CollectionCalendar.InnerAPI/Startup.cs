using Microsoft.OpenApi.Models;
using SFA.DAS.CollectionCalendar.DataAccess;
using SFA.DAS.CollectionCalendar.Infrastructure;
using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;
using SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authentication;
using SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authorization;
using SFA.DAS.CollectionCalendar.Queries;
using System.Reflection;

namespace SFA.DAS.CollectionCalendar.InnerAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration.BuildConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
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
            _configuration.Bind(nameof(ApplicationSettings), applicationSettings);
            services.AddDbContext<CollectionCalendarDataContext>();
            services.AddEntityFramework(applicationSettings, !_configuration.IsLocal());
            services.AddSingleton(x => applicationSettings);
            services.AddQueryServices();
            services.AddApplicationHealthChecks(applicationSettings);
            services.AddApiAuthentication(applicationSettings, _env.IsDevelopment());
            services.AddApiAuthorization(_env.IsDevelopment());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=EmployerIncentives}/{action=index}/{id?}");

                endpoints.MapHealthChecks("/ping");   // Both /ping 
                endpoints.MapHealthChecks("/");       // and / are used for health checks
            });



            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
