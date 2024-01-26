using Azure;
using Microsoft.EntityFrameworkCore;
using MoneyTransfer.Core.DTOs;
using MoneyTransfer.Core.Entities;
using MoneyTransfer.Core.Repositories;
using MoneyTransfer.Core.Services;
using MoneyTransfer.Core.UnitOfWork;
using MoneyTransfer.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IGenericRepository<Account> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomResponse<AccountDto>> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            var account = new Account
            {
                Money = createAccountDto.Money,
                AccountTypeId = createAccountDto.AccountTypeId,
                IsActive = true,
                AppUserId = createAccountDto.AppUserId,
            };
            var result = await _repository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();

            if (result == null)
            {
                return CustomResponse<AccountDto>.Fail(new ErrorDto("Hesap oluşturulamadı."), 400);
            }
            return CustomResponse<AccountDto>.Success(ObjectMapper.Mapper.Map<AccountDto>(account), 200);
        }
        public async Task<CustomResponse<List<AccountDto>>> GetUserAccountAsync(GetUserAccountsDto getUserAccountsDto)
        {
            var result = await _repository.Where(x => (x.AppUserId == getUserAccountsDto.AppUserId) && (x.IsActive == true)).ToListAsync();
            if (result.Count == 0)
            {
                return CustomResponse<List<AccountDto>>.Fail(new ErrorDto("Kullanıcıya ait hesap bulunamadı."), 400);
            }
            return CustomResponse<List<AccountDto>>.Success(ObjectMapper.Mapper.Map<List<AccountDto>>(result), 200);
        }

        public async Task<CustomResponse<AccountDto>> CloseAccountAsync(CloseAccountDto closeAccountDto)
        {
            var result = await _repository.Where(x => x.Id == closeAccountDto.AccountId && (x.IsActive == true)).SingleOrDefaultAsync();
            if (result == null)
            {
                return CustomResponse<AccountDto>.Fail(new ErrorDto("Böyle bir hesap yok"), 400);
            }

            if (result.Money > 0)
            {
                return CustomResponse<AccountDto>.Fail(new ErrorDto("Hesapta olan parayı başka hesabınıza aktarınız"), 400);
            }

            result.IsActive = false;
            await _unitOfWork.SaveChangesAsync();

            return CustomResponse<AccountDto>.Success(ObjectMapper.Mapper.Map<AccountDto>(result), 200);
        }

        public async Task<CustomResponse<NoDataDto>> TransferMoneyAsync(TransferMoneyDto transferMoneyDto)
        {
            var senderAccount = await _repository.Where(x => (x.Id == transferMoneyDto.SenderAccountId) && (x.IsActive == true)).SingleOrDefaultAsync();
            var destinationAccount = await _repository.Where(x => x.Id == transferMoneyDto.DestinationAccountId && (x.IsActive == true)).SingleOrDefaultAsync();
            if (senderAccount == null || destinationAccount == null)
            {
                return CustomResponse<NoDataDto>.Fail(new ErrorDto("Böyle bir hesap yok"), 400);
            }

            if (senderAccount.AccountTypeId!=destinationAccount.AccountTypeId)
            {
                return CustomResponse<NoDataDto>.Fail(new ErrorDto("Hesap tipleri farklı lütfen aynı tip hesap seçiniz"), 400);
            }

            if (senderAccount.Money < transferMoneyDto.Money)
            {
                return CustomResponse<NoDataDto>.Fail(new ErrorDto("Bakiyeniz yetersiz"), 400);
            }

            senderAccount.Money -= transferMoneyDto.Money;
            destinationAccount.Money += transferMoneyDto.Money;
            await _unitOfWork.SaveChangesAsync();
            return CustomResponse<NoDataDto>.Success(200);
        }

    }
}
