using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Web.Data;
using VLegalizer.Web.Helper;

namespace VLegalizer.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public EmployeesController(DataContext context,
               IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;

        }
   /*     [HttpPost]
        [Route("GetEmployeeByEmail")]
        public async Task<IActionResult> GetTrip([FromBody]EmailRequest emailRequest)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var tripEntity = await _context.Trips
                    .Include(t => t.Employee)
                    .Include(t => t.TripDetails)
                    .ThenInclude(e => e.ExpenseType)
                    .FirstOrDefaultAsync(t => t.Employee.Email.ToLower() == emailRequest.Email.ToLower());
                var response = new TripResponse
                {

                    Id = tripEntity.Id,
                    StartDate = tripEntity.StartDate,
                    EndDate = tripEntity.EndDate,
                    City = tripEntity.City,
                    TripDetails = tripEntity.TripDetails.Select(td => new TripDetailResponse
                    {
                        Id = td.Id,
                        Date = td.Date,
                        Amount = td.Amount,
                        PicturePath = td.ImageFullPath,
                        ExpenseType = td.ExpenseType.ExpenseNames
                    }).ToList()

                };

                return Ok(response);
            }
     }
     */
    }
}
