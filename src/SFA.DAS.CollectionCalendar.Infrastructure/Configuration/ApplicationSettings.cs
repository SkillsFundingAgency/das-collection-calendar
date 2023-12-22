using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.CollectionCalendar.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ApplicationSettings
    {
        public string DbConnectionString { get; set; } = null!;
    }
}
