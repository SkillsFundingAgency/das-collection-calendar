namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYear
{
    public class GetAcademicYearRequest : IQuery
    {
        public int Year { get; private set; }

        public GetAcademicYearRequest(int year)
        {
            Year = year;
        }
    }
}
