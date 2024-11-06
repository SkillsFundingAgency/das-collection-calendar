namespace SFA.DAS.CollectionCalendar.DataAccess.Entities
{
    [Table("dbo.AcademicYearDetails")]
    [System.ComponentModel.DataAnnotations.Schema.Table("AcademicYearDetails")]
    public class AcademicYearDetail
    {
        [Key]
        public string AcademicYear { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? HardCloseDate { get; set; }
    }
}