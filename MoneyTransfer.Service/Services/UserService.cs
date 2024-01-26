using AutoMapper.Internal.Mappers;
using Azure;
using Microsoft.AspNetCore.Identity;
using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Entities;
using MoneyTransfer.Core.Services;
using MoneyTransfer.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CustomResponse<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new AppUser { Email = createUserDto.Email,  UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponse<AppUserDto>.Fail(new ErrorDto(errors), 400);
            }
            return CustomResponse<AppUserDto>.Success(ObjectMapper.Mapper.Map<AppUserDto>(user), 200);
        }

        public async Task<CustomResponse<NoDataDto>> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user==null)
            {
                return CustomResponse<NoDataDto>.Fail(new ErrorDto("Böyle bir kullanıcı yok"), 400);
            }
            await _userManager.DeleteAsync(user);
            return CustomResponse<NoDataDto>.Success(200);
        }
    }
}
