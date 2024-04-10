using Microsoft.EntityFrameworkCore;
using SFA.DAS.CollectionCalendar.DataAccess.Entities;

namespace SFA.DAS.CollectionCalendar.DataAccess.Repositories
{
    public class AcademicYearQueryRepository : IAcademicYearQueryRepository
    {
        private readonly Lazy<CollectionCalendarDataContext> _lazyContext;
        private CollectionCalendarDataContext DbContext => _lazyContext.Value;

        public AcademicYearQueryRepository(Lazy<CollectionCalendarDataContext> dbContext)
        {
            _lazyContext = dbContext;
        }

        public async Task<AcademicYearDetail?> GetForDate(DateTime date)
        {
            var academicYearDetails = await DbContext.AcademicYearDetails
                .Where(x => x.StartDate <= date && x.EndDate >= date)
                .SingleOrDefaultAsync();

            return academicYearDetails;
        }
    }
}