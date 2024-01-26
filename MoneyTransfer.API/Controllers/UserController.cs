using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Services;

namespace MoneyTransfer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return ActionResultInstance(await _userService.DeleteUser(id));
        }

    }
}
