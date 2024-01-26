using MoneyTransfer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Services
{
    public interface IAccountService
    {
        Task<CustomResponse<AccountDto>> CreateAccountAsync(CreateAccountDto createAccountDto);
        Task<CustomResponse<List<AccountDto>>> GetUserAccountAsync(GetUserAccountsDto getUserAccountsDto);
        Task<CustomResponse<AccountDto>> CloseAccountAsync(CloseAccountDto closeAccountDto);
        Task<CustomResponse<NoDataDto>> TransferMoneyAsync(TransferMoneyDto transferMoneyDto);
    }
}
