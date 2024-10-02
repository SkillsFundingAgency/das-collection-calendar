using SFA.DAS.CollectionCalendar.Domain.Repositories;

namespace SFA.DAS.CollectionCalendar.Queries.GetAcademicYear
{
    public class GetAcademicYearQueryHandler : IQueryHandler<GetAcademicYearRequest, GetAcademicYearResponse>
    {
        private readonly IAcademicYearQueryRepository _academicYearQueryRepository;

        public GetAcademicYearQueryHandler(IAcademicYearQueryRepository academicYearQueryRepository)
        {
            _academicYearQueryRepository = academicYearQueryRepository;
        }

        public async Task<GetAcademicYearResponse> Handle(GetAcademicYearRequest query, CancellationToken cancellationToken = default)
        {
            var academicYear = await _academicYearQueryRepository.Get(query.Year);

            var response = new GetAcademicYearResponse(academicYear);

            return await Task.FromResult(response);
        }
    }
}