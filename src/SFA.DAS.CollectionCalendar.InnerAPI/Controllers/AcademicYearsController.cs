using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using SFA.DAS.CollectionCalendar.Queries;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYearForDate;

namespace SFA.DAS.CollectionCalendar.InnerAPI.Controllers
{
    [ApiController]
    public class AcademicYearsController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public AcademicYearsController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("academicyears/{date}")]
        [ProducesResponseType(typeof(AcademicYearDetails), 200)]
        public async Task<IActionResult> GetForDate(DateTime date)
        {
            var request = new GetAcademicYearForDateRequest(date);
            var response = await _queryDispatcher.Send<GetAcademicYearForDateRequest, GetAcademicYearForDateResponse>(request);

            if (response.AcademicYear == null)
            {
                return NotFound();
            }

            return Ok(response.AcademicYear);
        }
    }
}
