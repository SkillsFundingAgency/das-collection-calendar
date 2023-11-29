using SFA.DAS.CollectionCalendar.DataTransferObjects;

namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYearForDate
{
    public class GetAcademicYearForDateResponse
    {
        public AcademicYearDetails? AcademicYear { get; set; }

        public GetAcademicYearForDateResponse(AcademicYearDetails? academicYear)
        {
            AcademicYear = academicYear;
        }
    }
}
