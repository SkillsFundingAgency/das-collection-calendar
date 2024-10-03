using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataAccess.Entities;

namespace SFA.DAS.CollectionCalendar.DataAccess.UnitTests.AcademicYearQueryRepository
{
    public class WhenGettingAcademicYear
    {
        private Repositories.AcademicYearQueryRepository _sut;
        private Fixture _fixture;
        private CollectionCalendarDataContext _dbContext;

        [SetUp]
        public void Arrange()
        {
            _fixture = new Fixture();

            var options = new DbContextOptionsBuilder<CollectionCalendarDataContext>().UseInMemoryDatabase("CollectionCalendarDbContext" + Guid.NewGuid()).Options;
            _dbContext = new CollectionCalendarDataContext(options);

            _sut = new Repositories.AcademicYearQueryRepository(new Lazy<CollectionCalendarDataContext>(_dbContext));
        }

        [TearDown]
        public void CleanUp()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Then_the_correct_academicyear_is_retrieved()
        {
            var expectedAcademicYear = _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2324").Create();

            // Arrange
            var academicYearDetails = new AcademicYearDetail[]
            {
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2122").Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2223").Create(),
                expectedAcademicYear,
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2425").Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2526").Create(),
            };

            await _dbContext.AddRangeAsync(academicYearDetails);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sut.Get(2324);

            // Assert
            result.Should().NotBeNull();
            result.AcademicYear.Should().Be(expectedAcademicYear.AcademicYear);
            result.StartDate.Should().Be(expectedAcademicYear.StartDate);
            result.EndDate.Should().Be(expectedAcademicYear.EndDate);
            result.HardCloseDate.Should().Be(expectedAcademicYear.HardCloseDate);
        }

        [Test]
        public async Task Then_when_date_not_in_academic_year_null_returned()
        {
            // Arrange
            var academicYearDetails = new AcademicYearDetail[]
            {
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2122").Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2223").Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2425").Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.AcademicYear, "2526").Create(),
            };

            await _dbContext.AddRangeAsync(academicYearDetails);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sut.Get(2324);

            // Assert
            result.Should().BeNull();
        }
    }
}
