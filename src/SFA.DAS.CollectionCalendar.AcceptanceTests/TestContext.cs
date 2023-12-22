using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests
{
    public class TestContext
    {
        public string InstanceId { get; private set; }
        public CancellationToken CancellationToken { get; set; }
        public DirectoryInfo TestDirectory { get; set; }
        public SqlDatabase SqlDatabase { get; set; }
        public CollectionCalendarApi CollectionCalendarApi { get; set; }
        public TestWebApi EmployerIncentivesWebApiFactory { get; set; }

        public ApplicationSettings ApplicationSettings { get; set; }

        public TestContext()
        {
            InstanceId = Guid.NewGuid().ToString();
            TestDirectory = new DirectoryInfo(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, $"TestDirectory/{InstanceId}"));
            if (!TestDirectory.Exists)
            {
                Directory.CreateDirectory(TestDirectory.FullName);
            }
        }
    }
}
