using Microsoft.EntityFrameworkCore;
using SFA.DAS.CollectionCalendar.Domain.Repositories;

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

        public async Task<DataTransferObjects.AcademicYearDetails?> GetForDate(DateTime date)
        {
            var academicYearDetails = await DbContext.AcademicYearDetails
                .Where(x => x.StartDate <= date && x.EndDate >= date)
                .Select(x => new DataTransferObjects.AcademicYearDetails(x.AcademicYear, x.StartDate, x.EndDate, x.HardCloseDate))
                .SingleOrDefaultAsync();

            return academicYearDetails;
        }
    }
}