namespace SFA.DAS.CollectionCalendar.Infrastructure;

public interface ISqlAzureIdentityTokenProvider
{
    Task<string> GetAccessTokenAsync();
    string GetAccessToken();
}