using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using SFA.DAS.CollectionCalendar.InnerAPI.Controllers;
using SFA.DAS.CollectionCalendar.Queries;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYear;

namespace SFA.DAS.CollectionCalendar.InnerApi.UnitTests.Controllers.AcademicYearControllerTests
{
    public class WhenGet
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
            var year = _fixture.Create<int>();
            var expectedResult = _fixture.Create<GetAcademicYearResponse>();

            _queryDispatcher
                .Setup(x => x.Send<GetAcademicYearRequest, GetAcademicYearResponse>(It.Is<GetAcademicYearRequest>(r => r.Year == year)))
                .ReturnsAsync(expectedResult);

            var result = await _sut.Get(year);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedResult.AcademicYear);
        }

        [Test]
        public async Task ThenNotFoundReturnedWhenNoAcademicYear()
        {
            var year = _fixture.Create<int>();
            var expectedResult = _fixture.Build<GetAcademicYearResponse>().With(x => x.AcademicYear, (AcademicYearDetails?)null).Create();

            _queryDispatcher
                .Setup(x => x.Send<GetAcademicYearRequest, GetAcademicYearResponse>(It.Is<GetAcademicYearRequest>(r => r.Year == year)))
                .ReturnsAsync(expectedResult);

            var result = await _sut.Get(year);

            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}