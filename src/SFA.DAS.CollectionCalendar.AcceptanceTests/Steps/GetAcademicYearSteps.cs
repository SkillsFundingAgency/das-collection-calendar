using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using TechTalk.SpecFlow;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "GetAcademicYear")]
    public class GetAcademicYearSteps
    {
        private readonly TestContext _testContext;
        private HttpStatusCode _apiStatus;
        private AcademicYearDetails _apiData;

        public GetAcademicYearSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given("an academic year is requested for a valid year")]
        public async Task GivenAnAcademicYearIsRequestedByDate()
        {
            var year = 2324;
            var url = $"/academicyears/{year}";
            var (status, data) = await _testContext.CollectionCalendarApi.Client.GetValueAsync<AcademicYearDetails>(url);

            _apiStatus = status;
            _apiData = data;
        }

        [Then("the academic year details are returned")]
        public void ThenTheAcademicYearDetailsAreReturned()
        {
            _apiStatus.Should().Be(HttpStatusCode.OK);
            _apiData.AcademicYear.Should().Be("2324");
            _apiData.StartDate.Should().Be(new DateTime(2023, 8, 1));
            _apiData.EndDate.Should().Be(new DateTime(2024, 7, 31));
            _apiData.StartDate.Should().Be(new DateTime(2023, 8, 1));
        }
    }
}
