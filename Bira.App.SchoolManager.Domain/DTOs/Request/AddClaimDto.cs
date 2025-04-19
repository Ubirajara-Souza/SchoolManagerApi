
namespace Bira.App.SchoolManager.Domain.DTOs.Request
{
    public class AddClaimDto
    {
        public string Email { get; set; }
        public IEnumerable<ClaimItemDto> Claims { get; set; }
    }
}