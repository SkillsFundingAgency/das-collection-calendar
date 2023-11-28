namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYear
{
    public class GetAcademicYearRequest : IQuery
    {
        public string AcademicYear { get; private set; }

        public GetAcademicYearRequest(string academicYear)
        {
            AcademicYear = academicYear;
        }
    }
}
