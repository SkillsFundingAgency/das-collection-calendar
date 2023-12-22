using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests.Bindings
{
    [Binding]
    [Scope(Tag = "api")]
    public class Api
    {
        private readonly TestContext _context;

        public Api(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario(Order = 5)]
        public void InitialiseApi()
        {
            var webApi = new TestWebApi(_context);
            var options = new WebApplicationFactoryClientOptions
            {                
                BaseAddress = new System.Uri($"https://localhost:{GetAvailablePort(5001)}")
            };
            _context.EmployerIncentivesWebApiFactory = webApi;
            _context.CollectionCalendarApi = new CollectionCalendarApi(webApi.CreateClient(options));
        }

        [AfterScenario()]
        public async Task CleanUp()
        {
            _context.EmployerIncentivesWebApiFactory.Server?.Dispose();
            _context.EmployerIncentivesWebApiFactory.Dispose();
            _context.CollectionCalendarApi?.Dispose();            
        }

        public int GetAvailablePort(int startingPort)
        {
            if (startingPort > ushort.MaxValue) throw new ArgumentException($"Can't be greater than {ushort.MaxValue}", nameof(startingPort));
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            var connectionsEndpoints = ipGlobalProperties.GetActiveTcpConnections().Select(c => c.LocalEndPoint);
            var tcpListenersEndpoints = ipGlobalProperties.GetActiveTcpListeners();
            var udpListenersEndpoints = ipGlobalProperties.GetActiveUdpListeners();
            var portsInUse = connectionsEndpoints.Concat(tcpListenersEndpoints)
                                                 .Concat(udpListenersEndpoints)
                                                 .Select(e => e.Port);

            return Enumerable.Range(startingPort, ushort.MaxValue - startingPort + 1).Except(portsInUse).FirstOrDefault();
        }
    }
}
