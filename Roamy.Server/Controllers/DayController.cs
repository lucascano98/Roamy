using Roamy.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Roamy.Shared.Models;

namespace Roamy.Server.Controllers
{
    [ApiController]
    [Route("api/days")]
    public class DayController : ControllerBase
    {
        private readonly IDayRepository _dayRepository;
        public DayController(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        [HttpGet("/api/trips/{tripId}/days")]
        public async Task<IActionResult> GetAllDays(Guid tripId)
        {
            try
            {
                var days = await _dayRepository.GetAllDaysByTripAsync(tripId);
                return Ok(days);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDayById(Guid id)
        {
            try
            {
                var day = await _dayRepository.GetDayByIdAsync(id);
                if (day == null)
                    return NotFound();
                return Ok(day);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/api/trips/{tripId}/days")]
        public async Task<IActionResult> CreateDay(Guid tripId, [FromBody] Day day)
        {
            day.TripId = tripId;
            try
            {
                await _dayRepository.AddDayAsync(day);
                return CreatedAtAction(nameof(GetDayById), new { id = day.DayId }, day);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDay(Guid id, [FromBody] Day day)
        {
            if (id != day.DayId)
                return BadRequest("Id mismatch");
            try
            {
                await _dayRepository.UpdateDayAsync(day);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDay(Guid id)
        {
            try
            {
                var day = await _dayRepository.GetDayByIdAsync(id);
                if (day == null)
                    return NotFound();
                await _dayRepository.DeleteDayAsync(id);
                return Ok("Day successfully deleted");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
