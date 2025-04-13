using System.ComponentModel.DataAnnotations;

namespace Bira.App.SchoolManager.Domain.DTOs.Request
{
    public class StudentDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo Data de nascimento é obrigatório")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O campo Celular é obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O celular deve conter DDD + 9 dígitos (ex: 85912345678)")]
        public string CellPhone { get; set; }
        [Required(ErrorMessage = "O campo Codigo da escola é obrigatório")]
        public int CodeSchool { get; set; }
        public AddressDto Address { get; set; }
    }
}