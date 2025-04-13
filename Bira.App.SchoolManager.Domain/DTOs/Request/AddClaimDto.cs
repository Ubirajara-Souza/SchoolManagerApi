
namespace Bira.App.SchoolManager.Domain.DTOs.Request
{
    public class AddClaimDto
    {
        public string Email { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}