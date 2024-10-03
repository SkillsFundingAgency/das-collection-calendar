namespace SFA.DAS.CollectionCalendar.Domain.Repositories
{
    public interface IAcademicYearQueryRepository
    {
        Task<DataTransferObjects.AcademicYearDetails?> GetForDate(DateTime date);
        Task<DataTransferObjects.AcademicYearDetails?> Get(int year);
    }
}
