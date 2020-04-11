using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Models;

namespace VLegalizer.Web.Helper
{
    public class UserHelper : IUserHelper
    {

        private readonly UserManager<EmployeeEntity> _employeeManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<EmployeeEntity> _signInManager;

        public UserHelper(
            UserManager<EmployeeEntity> employeeManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<EmployeeEntity> signInManager

)
        {
            _employeeManager = employeeManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }

        public async Task<IdentityResult> AddUserAsync(EmployeeEntity user, string password)
        {
            return await _employeeManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(EmployeeEntity user, string roleName)
        {
            await _employeeManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }

        }

        public async Task<EmployeeEntity> GetUserByEmailAsync(string email)
        {
            return await _employeeManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsUserInRoleAsync(EmployeeEntity user, string roleName)
        {
            return await _employeeManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
