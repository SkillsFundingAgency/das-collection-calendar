using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using SFA.DAS.CollectionCalendar.InnerAPI.Controllers;
using SFA.DAS.CollectionCalendar.Queries;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYearForDate;

namespace SFA.DAS.CollectionCalendar.InnerApi.UnitTests.Controllers.AcademicYearControllerTests
{
    public class WhenGetByDate
    {
        private Fixture _fixture;
        private Mock<IQueryDispatcher> _queryDispatcher;
        private AcademicYearsController _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _queryDispatcher = new Mock<IQueryDispatcher>();
            _sut = new AcademicYearsController(_queryDispatcher.Object);
        }

        [Test]
        public async Task ThenAcademicYearsAreAreReturned()
        {
            var date = _fixture.Create<DateTime>();
            var expectedResult = _fixture.Create<GetAcademicYearForDateResponse>();

            _queryDispatcher
                .Setup(x => x.Send<GetAcademicYearForDateRequest, GetAcademicYearForDateResponse>(It.Is<GetAcademicYearForDateRequest>(r => r.Date == date)))
                .ReturnsAsync(expectedResult);

            var result = await _sut.GetForDate(date);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedResult.AcademicYear);
        }

        [Test]
        public async Task ThenNotFoundReturnedWhenNoAcademicYear()
        {
            var date = _fixture.Create<DateTime>();
            var expectedResult = _fixture.Build<GetAcademicYearForDateResponse>().With(x => x.AcademicYear, (AcademicYearDetails?)null).Create();

            _queryDispatcher
                .Setup(x => x.Send<GetAcademicYearForDateRequest, GetAcademicYearForDateResponse>(It.Is<GetAcademicYearForDateRequest>(r => r.Date == date)))
                .ReturnsAsync(expectedResult);

            var result = await _sut.GetForDate(date);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}