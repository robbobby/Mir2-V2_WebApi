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

        private readonly DbContextBroker context;
        public AccountRepository(DbContextBroker _context) {
            context = _context;
        }
        
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts() {
            return await context.Accounts.ToListAsync();
        }
        public async Task<AccountDbEntry> GetAccount(int _accountId = 1) {
            return await context.FindAsync<AccountDbEntry>(_accountId);
        }
        
        public async Task<AccountDbEntry> PostAccount(AccountDbEntry _accountDbEntry) {
            if (IsEmailAlreadyRegistered(_accountDbEntry.Email))
                return null;
            await context.Accounts.AddAsync(_accountDbEntry);
            await context.SaveChangesAsync();
            return null;
        }
        public void DeleteAccount(int _accountId) {
            context.Accounts.Remove(GetAccount(_accountId).Result);
            context.SaveChanges();
        }

        public bool IsEmailAlreadyRegistered(string _email) {
            return context.Accounts.Any(_account => _account.Email.Equals(_email));
        }
    }
}
