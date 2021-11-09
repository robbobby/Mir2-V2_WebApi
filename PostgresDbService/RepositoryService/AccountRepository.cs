using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database_Mir2_V2_WebApi.Broker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;
namespace Database_Mir2_V2_WebApi {
    public class AccountRepository : IDataAccessService<AccountDbEntry> {

        private readonly DbContextBroker _context;
        public AccountRepository(DbContextBroker context) {
            _context = context;
        }
        
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts() {
            return await _context.Accounts.ToListAsync();
        }
        public async Task<AccountDbEntry> GetAccount(int accountId = 1) {
            return await _context.FindAsync<AccountDbEntry>(accountId);
        }
        
        public async Task<AccountDbEntry> PostAccount(AccountDbEntry accountDbEntry) {
            if (IsEmailAlreadyRegistered(accountDbEntry.Email))
                return null;
            await _context.Accounts.AddAsync(accountDbEntry);
            await _context.SaveChangesAsync();
            return null;
        }
        public void DeleteAccount(int accountId) {
            _context.Accounts.Remove(GetAccount(accountId).Result);
            _context.SaveChanges();
        }

        public bool IsEmailAlreadyRegistered(string email) {
            return _context.Accounts.Any(account => account.Email.Equals(email));
        }
    }
}
