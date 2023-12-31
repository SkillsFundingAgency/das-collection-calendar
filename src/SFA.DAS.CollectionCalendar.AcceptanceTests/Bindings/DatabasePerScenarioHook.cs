﻿using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests.Bindings
{
    [Binding]
    [Scope(Tag = "database")]
    public class DatabasePerScenarioHook
    {
        [BeforeScenario(Order = 2)]
        public void CreateDatabase(TestContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            context.SqlDatabase = new SqlDatabase(context.InstanceId);
            stopwatch.Stop();
            Console.WriteLine($"[{nameof(DatabasePerScenarioHook)}] time it took to deploy test database: {stopwatch.Elapsed.Seconds} seconds");
        }

        [AfterScenario]
        public static void TearDownDatabase(TestContext context)
        {
            context.SqlDatabase?.Dispose();
        }
    }
}
