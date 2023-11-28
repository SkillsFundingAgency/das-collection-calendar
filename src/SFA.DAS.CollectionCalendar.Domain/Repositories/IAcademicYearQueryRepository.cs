namespace SFA.DAS.CollectionCalendar.Domain.Repositories
{
    public interface IAcademicYearQueryRepository
    {
        Task<DataTransferObjects.AcademicYear?> Get(string academicYear);
    }
}
