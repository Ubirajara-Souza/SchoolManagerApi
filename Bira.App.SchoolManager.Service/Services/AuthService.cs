using Bira.App.SchoolManager.Domain.DTOs.Request;
using Bira.App.SchoolManager.Domain.DTOs.Response;
using Bira.App.SchoolManager.Domain.Extensions;
using Bira.App.SchoolManager.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bira.App.SchoolManager.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public AuthService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings) : base()
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<IdentityResult> Register(RegisterUserDto registerUser)
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Name,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginUserDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            
            if (user is null)
                return SignInResult.Failed;

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginUser.Password, false, true);

            return result;
        }
        public async Task<LoginResponse> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await GenerateClaims(user);
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = GenerateToken(tokenHandler, identityClaims, key);
            var encodedToken = tokenHandler.WriteToken(token);

            var response = GenerateLoginResponse(encodedToken, user, claims);

            return response;
        }
        public async Task<IdentityResult> AddClaim(AddClaimDto addClaimDto)
        {
            var user = await _userManager.FindByEmailAsync(addClaimDto.Email);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var claim = new Claim(addClaimDto.ClaimType, addClaimDto.ClaimValue);
            var result = await _userManager.AddClaimAsync(user, claim);

            return result;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<IList<Claim>> GenerateClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            return claims;
        }

        private SecurityToken GenerateToken(JwtSecurityTokenHandler tokenHandler, ClaimsIdentity identityClaims, byte[] key)
        {
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return token;
        }

        private LoginResponse GenerateLoginResponse(string encodedToken, IdentityUser user, IList<Claim> claims)
        {
            var response = new LoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
