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

        public async Task<DataTransferObjects.AcademicYear?> Get(string academicYear)
        {
            /*var dataModels = await DbContext.Apprenticeships
                .Include(x => x.Approvals)
                .Where(x => x.Approvals.Any(x => x.UKPRN == ukprn && (fundingPlatform == null || x.FundingPlatform == fundingPlatform)))
                .ToListAsync();

            var result = dataModels.Select(x => new DataTransferObjects.Apprenticeship { Uln = x.Uln, LastName = x.LastName, FirstName = x.FirstName });
            return result; */
            throw new NotImplementedException();
        }
    }
}