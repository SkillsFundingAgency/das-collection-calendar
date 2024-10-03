using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using SFA.DAS.CollectionCalendar.Domain.Repositories;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYear;

namespace SFA.DAS.CollectionCalendar.Queries.UnitTests
{
    public class WhenGetAcademicYear
    {
        private Fixture _fixture;
        private Mock<IAcademicYearQueryRepository> _academicYearQueryRepository;
        private GetAcademicYearQueryHandler _sut;

        [SetUp]
        public void Setup()
        {

            _fixture = new Fixture();
            _academicYearQueryRepository = new Mock<IAcademicYearQueryRepository>();
            _sut = new GetAcademicYearQueryHandler(_academicYearQueryRepository.Object);
        }

        [Test]
        public async Task ThenAcademicYearIsReturned()
        {
            //Arrange
            var query = _fixture.Create<GetAcademicYearRequest>();
            var expectedResult = _fixture.Create<AcademicYearDetails>();

            _academicYearQueryRepository
                .Setup(x => x.Get(query.Year))
                .ReturnsAsync(expectedResult);

            //Act
            var actualResult = await _sut.Handle(query);

            //Assert
            actualResult.AcademicYear.Should().Be(expectedResult);
        }
    }
}