using FluentValidation;
using MoneyTransfer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Validations
{
    public class TransferMoneyDtoValidator : AbstractValidator<TransferMoneyDto>
    {
        public TransferMoneyDtoValidator()
        {
            RuleFor(x => x.SenderAccountId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
            RuleFor(x => x.DestinationAccountId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
            RuleFor(x => x.Money).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0 dan buyuk olmalı.");
        }
    }
}
