using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using TechTalk.SpecFlow;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "GetAcademicYearByDate")]
    public class GetAcademicYearByDateSteps
    {
        private readonly TestContext _testContext;
        private HttpStatusCode _apiStatus;
        private AcademicYearDetails _apiData;

        public GetAcademicYearByDateSteps(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Given("an academic year is requested for a valid date")]
        public async Task GivenAnAcademicYearIsRequestedByDate()
        {
            var date = new DateTime(2023, 11, 1);
            var url = $"/academicyears/{date.ToString("yyyy-MM-dd")}";
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
