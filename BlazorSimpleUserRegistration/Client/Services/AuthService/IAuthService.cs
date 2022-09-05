using BlazorSimpleUserRegistration.Shared;

namespace BlazorSimpleUserRegistration.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
    }
}
