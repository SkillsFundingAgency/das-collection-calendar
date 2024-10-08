using SFA.DAS.CollectionCalendar.DataTransferObjects;

namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYear
{
    public class GetAcademicYearResponse
    {
        public AcademicYearDetails? AcademicYear { get; set; }

        public GetAcademicYearResponse(AcademicYearDetails? academicYear)
        {
            AcademicYear = academicYear;
        }
    }
}
