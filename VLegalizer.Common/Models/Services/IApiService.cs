using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VLegalizer.Common.Models.Services
{
    public interface IApiService
    {
        Task<Response<EmployeeResponse>> GetTripByEmailAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);

        Task<Response<TokenResponse>> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);
    }

}
