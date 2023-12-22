namespace SFA.DAS.CollectionCalendar.Domain.Repositories
{
    public interface IAcademicYearQueryRepository
    {
        Task<DataTransferObjects.AcademicYearDetails?> GetForDate(DateTime date);
    }
}
