using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;
using TechTalk.SpecFlow;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests.Bindings
{
    [Binding]
    public class ApplicationSettingsHook
    {
        [BeforeScenario(Order = 3)]
        public void InitialiseApplicationSettings(TestContext context)
        {
            context.ApplicationSettings = new ApplicationSettings
            {
                DbConnectionString = context.SqlDatabase.DatabaseInfo.ConnectionString
            };
        }
    }
}
