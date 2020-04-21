using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Common.Models;
using VLegalizer.Web.Data;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Helper;

namespace VLegalizer.Web.Controllers.API
{
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;



        public AccountController(
            DataContext dataContext,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }



        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] EmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }



            EmployeeEntity employee = await _userHelper.GetUserByEmailAsync(request.Email);
            if (employee != null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is already registered."
                });
            }



            employee = new EmployeeEntity
            {
                Address = request.Address,
                Document = request.Document,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FixedPhone = request.FixedPhone,
                CellPhone = request.CellPhone,
                UserName = request.Email
            };



            IdentityResult result = await _userHelper.AddUserAsync(employee, request.Password);
            if (result != IdentityResult.Success)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }



            EmployeeEntity employeeNew = await _userHelper.GetUserByEmailAsync(request.Email);



            await _userHelper.AddUserToRoleAsync(employeeNew, "Employee");
            _dataContext.Employees.Add(new EmployeeEntity
            {
                Address = request.Address,
                Document = request.Document,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FixedPhone = request.FixedPhone,
                CellPhone = request.CellPhone,
                UserName = request.Email

            });
          


            await _dataContext.SaveChangesAsync();



            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(employee);
            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = employee.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);



            _mailHelper.SendMail(request.Email, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                $"To allow the user, " +
                $"please click on this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");



            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "A Confirmation email was sent. Please confirm your account and log into the App."
            });
        }



        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }



            EmployeeEntity user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is not assigned to any user."
                });
            }



            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            _mailHelper.SendMail(request.Email, "Password Reset", $"<h1>Recover Password</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");



            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "An email with instructions to change the password was sent."
            });
        }



        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutUser([FromBody] EmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var employeeEntity = await _userHelper.GetUserByEmailAsync(request.Email);
            if (employeeEntity == null)
            {
                return BadRequest("User not found.");
            }



            employeeEntity.FirstName = request.FirstName;
            employeeEntity.LastName = request.LastName;
            employeeEntity.Address = request.Address;
            employeeEntity.FixedPhone = request.FixedPhone;
            employeeEntity.CellPhone = request.CellPhone;
            employeeEntity.Document = request.Document;



            var respose = await _userHelper.UpdateUserAsync(employeeEntity);
            if (!respose.Succeeded)
            {
                return BadRequest(respose.Errors.FirstOrDefault().Description);
            }



            var updatedUser = await _userHelper.GetUserByEmailAsync(request.Email);
            return Ok(updatedUser);
        }



        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }



            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is not assigned to any user."
                });
            }



            var result = await _userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault().Description
                });
            }



            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "The password was changed successfully!"
            });
        }



    }
}
