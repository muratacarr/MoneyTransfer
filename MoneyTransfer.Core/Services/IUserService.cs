using MoneyTransfer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Services
{
    public interface IUserService
    {
        Task<CustomResponse<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponse<NoDataDto>> DeleteUser(int id);
    }
}
