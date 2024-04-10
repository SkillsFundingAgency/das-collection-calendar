using SFA.DAS.CollectionCalendar.DataAccess.Entities;

namespace SFA.DAS.CollectionCalendar.DataAccess.Repositories
{
    public interface IAcademicYearQueryRepository
    {
        Task<AcademicYearDetail?> GetForDate(DateTime date);
    }
}
