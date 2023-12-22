using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests.Bindings
{
    [Binding]
    public static class DatabasePerTestRunHook
    {
        [BeforeTestRun(Order = 1)]
        public static void RefreshDatabaseModel()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            SqlDatabaseModel.Update("SFA.DAS.CollectionCalendar.Database");
            stopwatch.Stop();
            Console.WriteLine($"[{nameof(DatabasePerTestRunHook)}] time it took to update `model` database: {stopwatch.Elapsed.Seconds} seconds");
        }
    }
}
