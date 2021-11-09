using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database_Mir2_V2_WebApi.Broker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
namespace Database_Mir2_V2_WebApi {
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
                return new AccountLoginResult(LoginResult.UserNameDoesNotExist, null);
            
            string hashedPassword = Hashing.HashPassword(accountDto.Password, account.Salt);
            if (!Hashing.ValidatePassword(hashedPassword, account.Password)) {
                return new AccountLoginResult(LoginResult.WrongPassword, null);
            }
            
            account.SessionToken = Guid.NewGuid();
            await _context.SaveChangesAsync();

            AccountLoginDtoS2C accountPayload = new AccountLoginDtoS2C(account.Id, account.SessionToken);
            return new AccountLoginResult(LoginResult.Success, accountPayload);
        }
        
        public async Task<AccountDbEntry> PostAccount(AccountDbEntry accountDbEntry) {
            if (IsEmailAlreadyRegistered(accountDbEntry.Email))
                return null;
            await _context.Accounts.AddAsync(accountDbEntry);
            await _context.SaveChangesAsync();
            return null;
        }
        public void DeleteAccount(int accountId) {
            // _context.Accounts.Remove(GetAccount(accountId).Result);
            _context.SaveChanges();
        }

        public bool IsEmailAlreadyRegistered(string email) {
            return _context.Accounts.Any(account => account.Email.Equals(email));
        }
    }
}
