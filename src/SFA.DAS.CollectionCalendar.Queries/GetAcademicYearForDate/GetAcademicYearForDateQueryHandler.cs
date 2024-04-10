using SFA.DAS.CollectionCalendar.DataAccess.Entities;
using SFA.DAS.CollectionCalendar.DataAccess.Repositories;
using SFA.DAS.CollectionCalendar.DataTransferObjects;

namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYearForDate
{
    public class GetAcademicYearForDateQueryHandler : IQueryHandler<GetAcademicYearForDateRequest, GetAcademicYearForDateResponse>
    {
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;

        public GetAcademicYearForDateQueryHandler(IAcademicYearQueryRepository academicYearQueryRepository)
        {
            _academicYearQueryRepository = academicYearQueryRepository;
        }

        public async Task<GetAcademicYearForDateResponse> Handle(GetAcademicYearForDateRequest query, CancellationToken cancellationToken = default)
        {
            var academicYear = await _academicYearQueryRepository.GetForDate(query.Date);

            var response = new GetAcademicYearForDateResponse(ToResponseType(academicYear));

            return await Task.FromResult(response);
        }

        private static AcademicYearDetails? ToResponseType(AcademicYearDetail? academicYearDetail)
        {
            if (academicYearDetail == null)
                return null;

            var lastDay = new DateTime(academicYearDetail.EndDate.Year, 6, DateTime.DaysInMonth(academicYearDetail.EndDate.Year, 6));

            while (lastDay.DayOfWeek != DayOfWeek.Friday)
            {
                lastDay = lastDay.AddDays(-1);
            }

            return new AcademicYearDetails(academicYearDetail.AcademicYear, academicYearDetail.StartDate, academicYearDetail.EndDate, academicYearDetail.HardCloseDate, lastDay);
        }
    }
}