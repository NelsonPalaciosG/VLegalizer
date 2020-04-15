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
                TripEntity tripEntity = await _context.Trips
                    .Include(t => t.Employee)
                    .Include(t => t.TripDetails)
                    .ThenInclude(e => e.ExpenseType)
                    .FirstOrDefaultAsync(t => t.Employee.Email.ToLower() == emailRequest.Email.ToLower());
                var response = new TripResponse
                {
                    Employee = new EmployeeResponse
                    {
                        Id = tripEntity.Employee.Id,
                        Document = tripEntity.Employee.Document,
                        FirstName = tripEntity.Employee.FirstName,
                        LastName = tripEntity.Employee.LastName,
                        FixedPhone = tripEntity.Employee.FixedPhone,
                        CellPhone = tripEntity.Employee.CellPhone,
                        Address = tripEntity.Employee.Address,
                        UserType = tripEntity.Employee.UserType,
                    },
                    Id = tripEntity.Id,
                    City = tripEntity.City,
                    StartDate = tripEntity.StartDate,
                    EndDate = tripEntity.EndDate,
                    TripDetails = tripEntity.TripDetails.Select(td => new TripDetailResponse
                    {
                        Id = td.Id,
                        Date = td.Date,
                        Description = td.Description,
                        Amount = td.Amount,
                        PicturePath = td.ImageFullPath,
                        ExpenseType = td.ExpenseType.ExpenseNames
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