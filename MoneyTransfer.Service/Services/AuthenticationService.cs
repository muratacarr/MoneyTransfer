using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Entities;
using MoneyTransfer.Core.Repositories;
using MoneyTransfer.Core.Services;
using MoneyTransfer.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponse<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return CustomResponse<TokenDto>.Fail("Email or Password is wrong", 400);
            var checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkPassword)
            {
                return CustomResponse<TokenDto>.Fail("Email or Password is wrong", 400);
            }
            var token = _tokenService.CreateToken(user);
            await _unitOfWork.SaveChangesAsync();

            return CustomResponse<TokenDto>.Success(token, 200);
        }
    }
}
