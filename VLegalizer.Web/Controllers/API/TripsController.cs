using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            List<TripEntity> trips = await _context.Trips
                .Include(tp => tp.TripDetails)
                .ThenInclude(tp => tp.ExpenseType)
                .Include(tp => tp.Employee)
                .OrderByDescending(tp => tp.Id)
                .ToListAsync();



            return Ok(_converterHelper.ToTripResponse(trips));
        }

        private bool TripEntityExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }


        [HttpPost]
        [Route("PostTrips")]
        public async Task<IActionResult> PostTrips([FromBody] TripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
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