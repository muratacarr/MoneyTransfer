using FluentValidation;
using MoneyTransfer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Validations
{
    public class GetUserAccountDtoValidator : AbstractValidator<GetUserAccountsDto>
    {
        public GetUserAccountDtoValidator()
        {
            RuleFor(x => x.AppUserId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
        }
    }
}
