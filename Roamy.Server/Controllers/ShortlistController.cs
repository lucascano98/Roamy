using Microsoft.AspNetCore.Mvc;
using Roamy.Server.Data;
using Roamy.Server.Repositories;
using Roamy.Shared.Models;

namespace Roamy.Server.Controllers
{
    [ApiController]
    [Route("api/trips/{tripId}/shortlist")]
    public class ShortlistController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;
        public ShortlistController (IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShortlistActivities(Guid tripId)
        {
            try
            {
                var shortlistActivities = await _activityRepository.GetShortlistByTripAsync(tripId);
                return Ok(shortlistActivities);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
