using System.ComponentModel.DataAnnotations;

namespace Bira.App.SchoolManager.Domain.DTOs.Request
{
    public class SchoolDto
    {
        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Description { get; set; }
    }
}