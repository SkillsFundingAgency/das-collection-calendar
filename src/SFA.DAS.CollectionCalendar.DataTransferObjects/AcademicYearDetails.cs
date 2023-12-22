namespace SFA.DAS.CollectionCalendar.DataTransferObjects
{
    public class AcademicYearDetails
    {
        public AcademicYearDetails(string academicYear, DateTime startDate, DateTime endDate, DateTime hardCloseDate)
        {
            AcademicYear = academicYear;
            StartDate = startDate;
            EndDate = endDate;
            HardCloseDate = hardCloseDate;
        }

        public string AcademicYear { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DateTime HardCloseDate { get; }
    }
}