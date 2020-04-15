using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VLegalizer.Common.Models.Services
{
    public interface IApiService
    {
        Task<Response> GetTripByEmailAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);

        Task<Response> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);
    }

}
