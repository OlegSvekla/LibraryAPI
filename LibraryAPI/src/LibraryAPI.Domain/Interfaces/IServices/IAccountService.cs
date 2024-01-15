using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Requests;
using LibraryAPI.Domain.Responses;

namespace LibraryAPI.Domain.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<UserDetailsResponse> Authenticate(AuthRegisterRequest model, string? ipAddress);

        Task<User> RegisterAsync<T>(T model, Role role) where T : AuthRegisterRequest;

        Task<User> VerifyEmailAsync(string token);
    }
}