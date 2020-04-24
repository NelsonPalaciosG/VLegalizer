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


        Task<Response> RegisterUserAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            EmployeeRequest employeeRequest);


        Task<Response> RecoverPasswordAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            EmailRequest emailRequest);

        Task<Response> PutAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken);

        Task<Response> RegisterTripAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model, 
            string tokenType,
            string accessToken);

        Task<Response> ChangePasswordAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            ChangePasswordRequest changePasswordRequest,
            string tokenType,
            string accessToken);


    }

}
