using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CollectionCalendar.DataTransferObjects;
using SFA.DAS.CollectionCalendar.Queries;
using SFA.DAS.CollectionCalendar.Queries.GetAcademicYear;

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
        [Route("academicyears/{academicYear}")]
        [ProducesResponseType(typeof(AcademicYear), 200)]
        public async Task<IActionResult> Get(string academicYear)
        {
            var request = new GetAcademicYearRequest(academicYear);
            var response = await _queryDispatcher.Send<GetAcademicYearRequest, GetAcademicYearResponse>(request);

            if (response.AcademicYear == null)
            {
                return NotFound();
            }

            return Ok(response.AcademicYear);
        }
    }
}
