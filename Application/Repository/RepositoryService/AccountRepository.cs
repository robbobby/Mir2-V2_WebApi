using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository.Broker;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.Enums;
namespace Application.Repository.RepositoryService {
    public class AccountRepository : IAccountAccessService<AccountDbEntry> {

        private readonly DbContextBroker _context;
        private IMapper _mapper;
        public AccountRepository(DbContextBroker context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts() {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<AccountLoginResult> GetAccount(AccountLoginDtoC2S accountDto) {
            AccountDbEntry account = await _context.Accounts.Where(account => account.UserName == accountDto.UserName).FirstAsync();
            if (account == null)
                return new AccountLoginResult(LoginResult.UserNameDoesNotExist);

            var hashedPassword = Hashing.HashPassword(accountDto.Password, account.Salt);
            if (!Hashing.ValidatePassword(hashedPassword, account.Password)) 
                return new AccountLoginResult(LoginResult.WrongPassword);
            if (account.IsLoggedIn)
                return new AccountLoginResult(LoginResult.AlreadyLoggedIn);

            account.SessionToken = Guid.NewGuid();
            account.IsLoggedIn = true;
            await _context.SaveChangesAsync();

            AccountLoginDto accountPayload = new(account.Id, account.SessionToken);
            return new AccountLoginResult(LoginResult.Success, accountPayload);
        }

        public async Task<AccountRegisterResult> PostAccount(AccountRegisterDtoC2S accountDbEntry) {
            if (IsEmailValid(accountDbEntry) != AccountRegisterResult.Ok) {
                return (AccountRegisterResult)IsEmailValid(accountDbEntry);
            }

            var salt = Hashing.GetRandomSalt();
            accountDbEntry.Password = Hashing.HashPassword(accountDbEntry.Password, salt);

            AccountDbEntry account = _mapper.Map<AccountDbEntry>(accountDbEntry);
            account.Salt = salt;
            account.CreatedOn = DateTime.Now;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return AccountRegisterResult.Success;
        }
        
        private AccountRegisterResult IsEmailValid(AccountRegisterDtoC2S accountDbEntry) {
            if (!new EmailAddressAttribute().IsValid(accountDbEntry.Email))
                return AccountRegisterResult.EmailNotValid;
            AccountRegisterResult uniqueEmailAndUserNameResult = IsEmailOrUserNameAlreadyRegistered(accountDbEntry.Email, accountDbEntry.UserName);
            if (uniqueEmailAndUserNameResult != AccountRegisterResult.Ok)
                return uniqueEmailAndUserNameResult;
            return AccountRegisterResult.Ok;
        }

        public void DeleteAccount(int accountId) {
            // _context.Accounts.Remove(GetAccount(accountId).Result);
            _context.SaveChanges();
        }

        public AccountRegisterResult IsEmailOrUserNameAlreadyRegistered(string email, string userName) {
            if (_context.Accounts.Any(account => account.Email.Equals(email)))
                return AccountRegisterResult.EmailAlreadyExists;
            if (_context.Accounts.Any(account => account.Email.Equals(userName)))
                return AccountRegisterResult.UserNameAlreadyExists;
            return AccountRegisterResult.Ok;
        }
    }
}
