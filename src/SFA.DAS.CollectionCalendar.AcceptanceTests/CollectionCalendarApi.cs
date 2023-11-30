using System;
using System.Net.Http;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests
{
    public class CollectionCalendarApi : IDisposable
    {
        public HttpClient Client { get; private set; }
        
        public Uri BaseAddress { get; private set; }
        private bool isDisposed;

        public CollectionCalendarApi(HttpClient client)
        {
            Client = client;
            BaseAddress = client.BaseAddress;
        }
      
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                Client.Dispose();
            }

            isDisposed = true;
        }
    }
}
