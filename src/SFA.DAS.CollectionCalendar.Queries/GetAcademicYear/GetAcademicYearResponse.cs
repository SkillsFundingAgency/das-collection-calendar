using SFA.DAS.CollectionCalendar.DataTransferObjects;

namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYear
{
    public class GetAcademicYearResponse
    {
        public AcademicYear? AcademicYear { get; set; }

        public GetAcademicYearResponse(AcademicYear? academicYear)
        {
            AcademicYear = academicYear;;
        }
    }
}
