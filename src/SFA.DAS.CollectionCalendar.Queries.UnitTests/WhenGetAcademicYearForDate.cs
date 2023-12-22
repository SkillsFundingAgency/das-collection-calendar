using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using SFA.DAS.CollectionCalendar.Domain.Repositories;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYearForDate;

namespace SFA.DAS.CollectionCalendar.Queries.UnitTests
{
    public class WhenGetAcademicYearForDate
    {
        private Fixture _fixture;
        private Mock<IAcademicYearQueryRepository> _academicYearQueryRepository;
        private GetAcademicYearForDateQueryHandler _sut;

        [SetUp]
        public void Setup()
        {

            _fixture = new Fixture();
            _academicYearQueryRepository = new Mock<IAcademicYearQueryRepository>();
            _sut = new GetAcademicYearForDateQueryHandler(_academicYearQueryRepository.Object);
        }

        [Test]
        public async Task ThenAcademicYearIsReturned()
        {
            //Arrange
            var query = _fixture.Create<GetAcademicYearForDateRequest>();
            var expectedResult = _fixture.Create<AcademicYearDetails>();

            _academicYearQueryRepository
                .Setup(x => x.GetForDate(query.Date))
                .ReturnsAsync(expectedResult);

            //Act
            var actualResult = await _sut.Handle(query);

            //Assert
            actualResult.AcademicYear.Should().Be(expectedResult);
        }
    }
}