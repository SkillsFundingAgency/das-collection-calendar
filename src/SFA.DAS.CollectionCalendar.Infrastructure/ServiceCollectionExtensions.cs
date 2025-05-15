using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.CollectionCalendar.DataAccess;
using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;
using SFA.DAS.CollectionCalendar.Infrastructure.HealthChecks;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.CollectionCalendar.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection services, ApplicationSettings settings, bool connectionNeedsAccessToken)
    {
        services.AddSingleton(new AzureServiceTokenProvider());

        services.AddSingleton<ISqlAzureIdentityTokenProvider, SqlAzureIdentityTokenProvider>();

        services.AddSingleton(provider => new SqlAzureIdentityAuthenticationDbConnectionInterceptor(provider.GetService<ILogger<SqlAzureIdentityAuthenticationDbConnectionInterceptor>>(), provider.GetService<ISqlAzureIdentityTokenProvider>(), connectionNeedsAccessToken));

        services.AddScoped(p =>
        {
            var options = new DbContextOptionsBuilder<CollectionCalendarDataContext>()
                .UseSqlServer(new SqlConnection(settings.DbConnectionString), optionsBuilder => optionsBuilder.CommandTimeout(7200)) //7200=2hours
                .AddInterceptors(p.GetRequiredService<SqlAzureIdentityAuthenticationDbConnectionInterceptor>())
                .Options;
            return new CollectionCalendarDataContext(options);
        });

        return services.AddScoped(provider => new Lazy<CollectionCalendarDataContext>(provider.GetService<CollectionCalendarDataContext>() ?? throw new ArgumentException("CollectionCalendarDataContext")));
    }

    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, ApplicationSettings applicationSettings, bool sqlConnectionNeedsAccessToken)
    {
        services.AddSingleton(sp => new DbHealthCheck(
            applicationSettings.DbConnectionString, 
            sp.GetService<ILogger<DbHealthCheck>>()!, 
            sp.GetSqlAzureIdentityTokenProvider(sqlConnectionNeedsAccessToken)));
        services.AddHealthChecks().AddCheck<DbHealthCheck>("Database");
        return services;
    }
}
