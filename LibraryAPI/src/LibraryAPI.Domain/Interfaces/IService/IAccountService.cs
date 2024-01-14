using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Request;
using LibraryAPI.Domain.Response;

namespace LibraryAPI.Domain.Interfaces.IService
{
    public interface IAccountService
    {
        Task<UserDetailsResponse> Authenticate(BaseAuhtRequest model, string? ipAddress);

        Task<User> RegisterAsync<T>(T model, Role role) where T : RegisterRequest;

        Task<User> VerifyEmailAsync(string token);
    }
}