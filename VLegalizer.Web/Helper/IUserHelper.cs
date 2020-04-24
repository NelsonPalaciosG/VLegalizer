using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Models;

namespace VLegalizer.Web.Helper
{
    public interface IUserHelper
    {
        Task<EmployeeEntity> GetUserByEmailAsync(string email);

        Task<EmployeeEntity> GetUserAsync(Guid employeeId);

        Task<EmployeeEntity> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(EmployeeEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(EmployeeEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(EmployeeEntity user, string roleName); 

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<SignInResult> ValidatePasswordAsync(EmployeeEntity user, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(EmployeeEntity employee);

        Task<IdentityResult> ConfirmEmailAsync(EmployeeEntity user, string token);

        Task<string> GeneratePasswordResetTokenAsync(EmployeeEntity user);

        Task<IdentityResult> UpdateUserAsync(EmployeeEntity user);

        Task<IdentityResult> ChangePasswordAsync(EmployeeEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> ResetPasswordAsync(EmployeeEntity user, string token, string password);
    }
}
