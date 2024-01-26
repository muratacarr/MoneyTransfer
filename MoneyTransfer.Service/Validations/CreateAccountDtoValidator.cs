using FluentValidation;
using MoneyTransfer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Validations
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator()
        {
            RuleFor(x => x.Money).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
            RuleFor(x=>x.AccountTypeId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
            RuleFor(x=>x.AppUserId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
        }
    }
}
