using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLegalizer.Web.Data.Entities;
using VLegalizer.Web.Models;

namespace VLegalizer.Web.Helper
{
    public interface IUserHelper
    {
        Task<EmployeeEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(EmployeeEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(EmployeeEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(EmployeeEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

    }
}
