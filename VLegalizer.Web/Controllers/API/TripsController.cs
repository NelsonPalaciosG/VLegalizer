using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Common.Models;
using VLegalizer.Web.Data;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Helper;

namespace VLegalizer.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context,
               IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;

        }

        [HttpPost]
        [Route("GetTripByEmail")]
        public async Task<IActionResult> GetTrip([FromBody]EmailRequest emailRequest)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var employeeEntity = await _context.Employees
                    .Include(e => e.Trips)
                    .ThenInclude(t => t.TripDetails)
                    .ThenInclude(et => et.ExpenseType)
                    .FirstOrDefaultAsync(e => e.Email.ToLower() == emailRequest.Email.ToLower());
                var response = new EmployeeResponse
                {   
                        Document = employeeEntity.Document,
                        FirstName = employeeEntity.FirstName,
                        LastName = employeeEntity.LastName,
                        FixedPhone = employeeEntity.FixedPhone,
                        CellPhone = employeeEntity.CellPhone,
                        Address = employeeEntity.Address,
                        UserType = employeeEntity.UserType,
                    
                    Trips = employeeEntity.Trips.Select(t => new TripResponse
                    {
                        Id = t.Id,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        City = t.City,
                        TripDetails = t.TripDetails.Select(td => new TripDetailResponse
                        {
                            Id = td.Id,
                            Date = td.Date,
                            Amount = td.Amount,
                            PicturePath = td.ImageFullPath,
                            ExpenseType = td.ExpenseType.ExpenseNames
                        }).ToList(),
                    }).ToList()
                };

                return Ok(response);
            }
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TripEntity tripEntity = await _context.Trips.FindAsync(id);

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

            TripEntity tripEntity = await _context.Trips.FindAsync(id);
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