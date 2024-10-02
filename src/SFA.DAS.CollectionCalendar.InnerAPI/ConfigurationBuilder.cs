using SFA.DAS.Configuration.AzureTableStorage;

namespace SFA.DAS.CollectionCalendar.InnerAPI
{
    public static class ConfigurationBuilderExtension
    {
        public static IConfigurationRoot BuildConfiguration(this IConfiguration configuration)
        {
            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();
#if DEBUG
            config.AddJsonFile("appsettings.json", true);
            if (!configuration.IsLocalAcceptanceTests())
            {
                config.AddJsonFile("appsettings.Development.json", true);
            }
#endif
            if (!configuration.IsLocalAcceptanceTestsOrDev())
            {
                config.AddAzureTableStorage(options =>
                    {
                        options.ConfigurationKeys = new[] { "SFA.DAS.CollectionCalendar" };
                        options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                        options.EnvironmentName = configuration["EnvironmentName"];
                        options.PreFixConfigurationKeys = false;
                    }
                );
            }

            return config.Build();
        }

        public static bool IsLocalOrDev(this IConfiguration configuration)
        {
            return configuration.IsLocal() || configuration.IsDev() || configuration.IsLocalAcceptanceTests();
        }

        public static bool IsLocalAcceptanceTestsOrDev(this IConfiguration configuration)
        {
            return configuration.IsLocalAcceptanceTests() || configuration.IsDev();
        }

        public static bool IsLocalAcceptanceTests(this IConfiguration configuration)
        {
            return configuration["EnvironmentName"].Equals("LOCAL_ACCEPTANCE_TESTS", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsDev(this IConfiguration configuration)
        {
            return configuration["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
        }
        public static bool IsLocal(this IConfiguration configuration)
        {
            return configuration["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}