namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYearForDate
{
    public class GetAcademicYearForDateRequest : IQuery
    {
        public DateTime Date { get; private set; }

        public GetAcademicYearForDateRequest(DateTime date)
        {
            Date = date;
        }
    }
}
