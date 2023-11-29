using SFA.DAS.CollectionCalendar.Domain.Repositories;

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

            var response = new GetAcademicYearForDateResponse(academicYear);

            return await Task.FromResult(response);
        }
    }
}