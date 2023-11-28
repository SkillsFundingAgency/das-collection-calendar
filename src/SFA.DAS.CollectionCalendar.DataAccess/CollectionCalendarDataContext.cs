using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.CollectionCalendar.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class CollectionCalendarDataContext : DbContext
    {
        public CollectionCalendarDataContext(DbContextOptions<CollectionCalendarDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
