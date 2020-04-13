using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VLegalizer.Web.Data;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;

        public TripsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<TripEntity> GetTrips()
        {
            return _context.Trips;
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntity = await _context.Trips.FindAsync(id);

            if (tripEntity == null)
            {
                return NotFound();
            }

            return Ok(tripEntity);
        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripEntity([FromRoute] int id, [FromBody] TripEntity tripEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tripEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(tripEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripEntity tripEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Add(tripEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripEntity", new { id = tripEntity.Id }, tripEntity);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntity = await _context.Trips.FindAsync(id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(tripEntity);
            await _context.SaveChangesAsync();

            return Ok(tripEntity);
        }

        private bool TripEntityExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}