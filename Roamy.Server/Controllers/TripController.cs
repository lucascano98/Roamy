using Microsoft.AspNetCore.Mvc;
using Roamy.Server.Repositories;
using Roamy.Shared.Models;

namespace Roamy.Server.Controllers
{
    [ApiController]
    [Route("api/trips")]
    // ControllerBase instead of Controller — no View support needed, API returns JSON only
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            try
            {
                var trips = await _tripRepository.GetAllTripsAsync();
                return Ok(trips);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //enter the {id} to include in the routing
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(Guid id)
        {
            try
            {
                var trip = await _tripRepository.GetTripByIdAsync(id);
                if (trip == null)
                    return NotFound();
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] Trip trip)
        {
            try
            {
                await _tripRepository.AddTripAsync(trip);
                //Status Code 201 - Created
                return CreatedAtAction(nameof(GetTripById), new { id = trip.TripId }, trip);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(Guid id, [FromBody] Trip trip)
        {
            if (id != trip.TripId)
                return BadRequest("ID mismatch"); //Error 400
            try
            {
                await _tripRepository.UpdateTripAsync(trip);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            try
            {
                var trip = await _tripRepository.GetTripByIdAsync(id);
                if (trip == null)
                    return NotFound();
                await _tripRepository.DeleteTripAsync(id);
                return Ok("Trip successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
