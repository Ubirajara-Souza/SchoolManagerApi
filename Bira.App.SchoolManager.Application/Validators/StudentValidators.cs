using Bira.App.SchoolManager.Domain.Entities;
using FluentValidation;

namespace Bira.App.SchoolManager.Application.Validators
{
    public class StudentValidators : AbstractValidator<Student>
    {
        public StudentValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve conter 11 dígitos numéricos")
                .Must(IsValidCpf).WithMessage("CPF inválido");

            RuleFor(x => x.CellPhone)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Matches(@"^\d{11}$").WithMessage("O celular deve conter DDD + 9 dígitos (ex: 85912345678)");

            RuleFor(x => x.CodeSchool)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || new string(cpf[0], 11) == cpf)
                return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * mult1[i];

            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;

            tempCpf += firstDigit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * mult2[i];

            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;

            return cpf.EndsWith($"{firstDigit}{secondDigit}");
        }
    }
}