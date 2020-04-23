using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Helper;
using VLegalizer.Web.Models;

namespace VLegalizer.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;


        public AccountController(
            IUserHelper userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper
            )
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Data.Entities.EmployeeEntity user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        Claim[] claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }
        /* esto lo usaremos para la web
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Register(AddUserViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        string path = string.Empty;

                        if (model.PictureFile != null)
                        {
                            path = await _imageHelper.UploadImageAsync(model.PictureFile, "Users");
                        }

                        UserEntity user = await _userHelper.AddUserAsync(model, path, UserType.User);
                        if (user == null)
                        {
                            ModelState.AddModelError(string.Empty, "This email is already used.");
                            model.Teams = _combosHelper.GetComboTeams();
                            return View(model);
                        }

                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                        var tokenLink = Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        var response = _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                            $"To allow the user, " +
                            $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                        if (response.IsSuccess)
                        {
                            ViewBag.Message = "The instructions to allow your user has been sent to email.";
                            return View(model);
                        }

                        ModelState.AddModelError(string.Empty, response.Message);
                    }

                    model.Teams = _combosHelper.GetComboTeams();
                    return View(model);
                }
        */

        public async Task<IActionResult> ConfirmEmail(string employeeId, string token)
        {
            if (string.IsNullOrEmpty(employeeId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            EmployeeEntity employee = await _userHelper.GetUserAsync(new Guid(employeeId));
            if (employee == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(employee, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }



    }


}
