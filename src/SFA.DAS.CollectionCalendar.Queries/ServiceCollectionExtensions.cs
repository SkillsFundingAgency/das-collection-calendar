using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.CollectionCalendar.DataAccess.Repositories;
using SFA.DAS.CollectionCalendar.Domain.Repositories;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYear;

namespace SFA.DAS.CollectionCalendar.Queries
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .Scan(scan =>
                {
                    scan.FromExecutingAssembly()
                        .AddClasses(classes => classes.AssignableTo(typeof(IQuery)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();

                    scan.FromAssembliesOf(typeof(GetAcademicYearQueryHandler))
                        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();
                })
                .AddTransient<IAcademicYearQueryRepository, AcademicYearQueryRepository>()
                .AddScoped<IQueryDispatcher, QueryDispatcher>();

            return serviceCollection;
        }
    }
}