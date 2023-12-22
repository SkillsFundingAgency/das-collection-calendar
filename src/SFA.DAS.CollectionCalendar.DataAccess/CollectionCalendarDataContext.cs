using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CollectionCalendar.DataAccess.Entities;

namespace SFA.DAS.CollectionCalendar.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class CollectionCalendarDataContext : DbContext
    {
        public CollectionCalendarDataContext(DbContextOptions<CollectionCalendarDataContext> options) : base(options)
        {
        }

        public virtual DbSet<AcademicYearDetail> AcademicYearDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicYearDetail>()
                .HasKey(a => new { a.AcademicYear });

            base.OnModelCreating(modelBuilder);
        }
    }
}
