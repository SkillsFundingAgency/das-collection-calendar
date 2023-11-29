using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataAccess.Entities;

namespace SFA.DAS.CollectionCalendar.DataAccess.UnitTests.AcademicYearQueryRepository
{
    public class WhenGettingAcademicYearForDate
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
        public async Task Then_the_correct_academicyear_for_the_date_is_retrieved()
        {
            var date = new DateTime(2023, 11, 29);
            var expectedAcademicYear = _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2023, 8, 1)).With(x => x.EndDate, new DateTime(2024, 7, 31)).Create();

            // Arrange
            var academicYearDetails = new AcademicYearDetail[]
            {
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2021, 8, 1)).With(x => x.EndDate, new DateTime(2022, 7, 31)).Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2022, 8, 1)).With(x => x.EndDate, new DateTime(2023, 7, 31)).Create(),
                expectedAcademicYear,
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2024, 8, 1)).With(x => x.EndDate, new DateTime(2025, 7, 31)).Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2025, 8, 1)).With(x => x.EndDate, new DateTime(2026, 7, 31)).Create(),
            };

            await _dbContext.AddRangeAsync(academicYearDetails);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sut.GetForDate(date);

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
            var date = new DateTime(2026, 11, 29);

            // Arrange
            var academicYearDetails = new AcademicYearDetail[]
            {
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2021, 8, 1)).With(x => x.EndDate, new DateTime(2022, 7, 31)).Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2022, 8, 1)).With(x => x.EndDate, new DateTime(2023, 7, 31)).Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2023, 8, 1)).With(x => x.EndDate, new DateTime(2024, 7, 31)).Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2024, 8, 1)).With(x => x.EndDate, new DateTime(2025, 7, 31)).Create(),
                _fixture.Build<AcademicYearDetail>().With(x => x.StartDate, new DateTime(2025, 8, 1)).With(x => x.EndDate, new DateTime(2026, 7, 31)).Create(),
            };

            await _dbContext.AddRangeAsync(academicYearDetails);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _sut.GetForDate(date);

            // Assert
            result.Should().BeNull();
        }
    }
}
