using Roamy.Shared.Models;
using Roamy.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Roamy.Server.Repositories;

namespace Roamy.Server.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpGet("/api/days/{dayId}/activities")]
        public async Task<IActionResult> GetAllActivitiesByDay(Guid dayId)
        {
            try
            {
                var activities = await _activityRepository.GetAllActivitiesByDayAsync(dayId);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            try
            {
                var activity = await _activityRepository.GetActivityByIdAsync(id);
                if (activity == null)
                    return NotFound();
                return Ok(activity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/api/days/{dayId}/activities")]
        public async Task<IActionResult> CreateActivity(Guid dayId, [FromBody] Activity activity)
        {
            activity.DayId = dayId;
            try
            {
                await _activityRepository.AddActivityAsync(activity);
                return CreatedAtAction(nameof(GetActivityById), new { id = activity.ActivityId }, activity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] Activity activity)
        {
            if (id != activity.ActivityId)
                return BadRequest("Id mismatch");
            try
            {
                await _activityRepository.UpdateActivityAsync(activity);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            try
            {
                var activity = await _activityRepository.GetActivityByIdAsync(id);
                if (activity == null)
                    return NotFound();
                await _activityRepository.DeleteActivityAsync(id);
                return Ok("Activity successfully deleted");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
