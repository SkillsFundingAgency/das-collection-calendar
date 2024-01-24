using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.CollectionCalendar.Infrastructure.Configuration;

[ExcludeFromCodeCoverage]
public class AzureActiveDirectoryConfiguration
{
    public string Identifier { get; set; } = null!;
    public string Tenant { get; set; } = null!;
}