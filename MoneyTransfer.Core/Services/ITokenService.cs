using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser appUser);
    }
}
