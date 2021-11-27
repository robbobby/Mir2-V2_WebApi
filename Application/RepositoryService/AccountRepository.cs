using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database_Mir2_V2_WebApi.Broker;
using Microsoft.EntityFrameworkCore;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.Enums;

namespace Database_Mir2_V2_WebApi.RepositoryService {
    public class AccountRepository : IDataAccessService<AccountDbEntry> {

        private readonly DbContextBroker _context;
        public AccountRepository(DbContextBroker context) {
            _context = context;
        }

        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts() {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<AccountLoginResult> GetAccount(AccountLoginDtoC2S accountDto) {
            AccountDbEntry account = await _context.Accounts.Where(account => account.UserName == accountDto.UserName).FirstAsync();
            if (account == null)
                return new AccountLoginResult(LoginResult.UserNameDoesNotExist);

            var hashedPassword = Hashing.HashPassword(accountDto.Password, account.Salt);
            if (!Hashing.ValidatePassword(hashedPassword, account.Password)) return new AccountLoginResult(LoginResult.WrongPassword);
            if (account.IsLoggedIn)
                return new AccountLoginResult(LoginResult.AlreadyLoggedIn);

            account.SessionToken = Guid.NewGuid();
            account.IsLoggedIn = true;
            await _context.SaveChangesAsync();

            AccountLoginDtoS2C accountPayload = new(account.Id, account.SessionToken);
            return new AccountLoginResult(LoginResult.Success, accountPayload);
        }

        public async Task<AccountDbEntry> PostAccount(AccountDbEntry accountDbEntry) {
            if (IsEmailOrUserNameAlreadyRegistered(accountDbEntry.Email, accountDbEntry.UserName) != AccountRegisterResult.Success)
                return null;
            await _context.Accounts.AddAsync(accountDbEntry);
            await _context.SaveChangesAsync();
            return null;
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
            return AccountRegisterResult.Success;
        }
    }
}
