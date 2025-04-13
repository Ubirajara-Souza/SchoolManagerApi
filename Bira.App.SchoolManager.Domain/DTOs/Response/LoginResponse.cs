using Bira.App.SchoolManager.Domain.DTOs.Request;

namespace Bira.App.SchoolManager.Domain.DTOs.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; }
    }
}