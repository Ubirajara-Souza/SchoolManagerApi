using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;
using Microsoft.AspNetCore.Identity;

namespace Bira.App.SchoolManager.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> GenerateJwt(string email);
        Task<SignInResult> Login(LoginUserDto loginUser);
        Task<LoginResponse> RefreshToken(RefreshTokenUserDto refreshTokenUser);
        Task<IdentityResult> Register(RegisterUserDto registerUser);
        Task<IdentityResult> AddClaim(AddClaimDto addClaimDto);
        Task Logout();
    }
}