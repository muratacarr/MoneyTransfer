using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Services;

namespace MoneyTransfer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : CustomBaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountDto createAccountDto)
        {
            return ActionResultInstance(await _accountService.CreateAccountAsync(createAccountDto));
        }
        [HttpPost]
        public async Task<IActionResult> GetAccount(GetUserAccountsDto getUserAccountsDto)
        {
            return ActionResultInstance(await _accountService.GetUserAccountAsync(getUserAccountsDto));
        }
        [HttpPost]
        public async Task<IActionResult> CloseAccount(CloseAccountDto closeAccountDto)
        {
            return ActionResultInstance(await _accountService.CloseAccountAsync(closeAccountDto));
        }
        [HttpPost]
        public async Task<IActionResult> TransferMoney(TransferMoneyDto transferMoneyDto)
        {
            return ActionResultInstance(await _accountService.TransferMoneyAsync(transferMoneyDto));
        }

    }
}
