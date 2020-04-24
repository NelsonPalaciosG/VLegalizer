using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Common.Models;
using VLegalizer.Prism.Resources;
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
        private readonly IUserHelper _userHelper;

        public TripsController(DataContext context,
               IConverterHelper converterHelper,
                IUserHelper userHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;

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

                var EmployeeEntity = await _context.Employees
                    .Include(t => t.Trips)
                    .ThenInclude(td => td.TripDetails)
                    .ThenInclude(e => e.ExpenseType)
                    .FirstOrDefaultAsync(t => t.Email.ToLower() == emailRequest.Email.ToLower());

                var response = new EmployeeResponse
                {
                    Document = EmployeeEntity.Document,
                    FirstName = EmployeeEntity.FirstName,
                    LastName = EmployeeEntity.LastName,
                    FixedPhone = EmployeeEntity.FixedPhone,
                    CellPhone = EmployeeEntity.CellPhone,
                    Address = EmployeeEntity.Address,
                    UserType = EmployeeEntity.UserType,
                    Trips = EmployeeEntity.Trips?.Select(t => new TripResponse
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
                            Description = td.Description,
                            PicturePath = td.ImageFullPath,
                            IdExpenseType = td.ExpenseType.Id,
                            ExpenseName = td.ExpenseType.ExpenseNames
                        }).ToList()
                    }).ToList(),
                };

                return Ok(response);
            }
        }

       /* [HttpPost]
        [Route("GetMyTrips")]
        public async Task<IActionResult> GetMyTripsAsync([FromBody] MyTripsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntity = await _context.Trips
                .Include(t => t.Employee)
                .Include(t => t.TripDetails)
                .ThenInclude(t => t.TripDetails)
                .Where(t => t.Employee.Email == request.Email &&
                            t.StartDate >= request.StartDate)
                .OrderByDescending(t => t.StartDate)
                .ToListAsync();

            return Ok(_converterHelper.ToTripResponse(tripEntity));
        }*/

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

        [HttpPost]
        public async Task<IActionResult> PostTrips([FromBody] TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }


            EmployeeEntity employeeEntity = await _userHelper.GetUserAsync(request.EmployeeId);
            if (employeeEntity == null)
            {
                return BadRequest(Resource.UserdontExist);
            }




            TripEntity tripEntity = new TripEntity
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                City = request.City,
                Employee = employeeEntity
            };



            _context.Trips.Add(tripEntity);
            try
            {
                await _context.SaveChangesAsync();



            }

            catch (Exception ex)
            {
                ex.ToString();
            }



            return Ok(_converterHelper.ToTripResponse(tripEntity));
        }

    }
}