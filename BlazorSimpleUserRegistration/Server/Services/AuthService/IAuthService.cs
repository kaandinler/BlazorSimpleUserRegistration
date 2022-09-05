using BlazorSimpleUserRegistration.Shared;

namespace BlazorSimpleUserRegistration.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> IsUserExists(string email);
    }
}
