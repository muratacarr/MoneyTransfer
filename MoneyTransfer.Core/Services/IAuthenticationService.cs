using MoneyTransfer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<CustomResponse<TokenDto>> CreateTokenAsync(LoginDto loginDto);
    }
}
