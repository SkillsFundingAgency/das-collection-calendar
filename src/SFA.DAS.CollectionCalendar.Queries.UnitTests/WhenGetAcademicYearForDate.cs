using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataAccess.Entities;
using SFA.DAS.CollectionCalendar.DataAccess.Repositories;
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
            var academicYear = _fixture.Create<AcademicYearDetail>();

            _academicYearQueryRepository
                .Setup(x => x.GetForDate(query.Date))
                .ReturnsAsync(academicYear);

            //Act
            var actualResult = await _sut.Handle(query);

            //Assert
            actualResult.AcademicYear?.AcademicYear.Should().Be(academicYear.AcademicYear);
            actualResult.AcademicYear?.StartDate.Should().Be(academicYear.StartDate);
            actualResult.AcademicYear?.EndDate.Should().Be(academicYear.EndDate);
            actualResult.AcademicYear?.HardCloseDate.Should().Be(academicYear.HardCloseDate);
        }

        [TestCase(2024, 28)]
        [TestCase(2028, 30)]
        [TestCase(2029, 29)]

        public async Task ThenLastFridayInJuneIsCalculatedCorrectly(int year, int expectedDate)
        {
            //Arrange
            var query = _fixture.Create<GetAcademicYearForDateRequest>();
            var academicYear = _fixture.Create<AcademicYearDetail>();
            academicYear.EndDate = new DateTime(year, 7, 31);

            _academicYearQueryRepository
                .Setup(x => x.GetForDate(query.Date))
                .ReturnsAsync(academicYear);

            //Act
            var actualResult = await _sut.Handle(query);

            //Assert
            actualResult.AcademicYear?.LastFridayInJune.Year.Should().Be(year);
            actualResult.AcademicYear?.LastFridayInJune.Month.Should().Be(6);
            actualResult.AcademicYear?.LastFridayInJune.Day.Should().Be(expectedDate);
        }
    }
}