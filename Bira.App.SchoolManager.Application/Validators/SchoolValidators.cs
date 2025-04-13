using Bira.App.SchoolManager.Domain.Entities;
using FluentValidation;

namespace Bira.App.SchoolManager.Application.Validators
{
    public class SchoolValidators : AbstractValidator<School>
    {
        public SchoolValidators()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}