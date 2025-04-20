using System.ComponentModel.DataAnnotations;

namespace Bira.App.SchoolManager.Domain.DTOs.Request
{
    public class RefreshTokenUserDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
    }
}