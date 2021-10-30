using System.Collections.Generic;
using System.Threading.Tasks;
using Database_Mir2_V2_WebApi.Broker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;
namespace Database_Mir2_V2_WebApi {
    public class AccountRepository : IDataAccess {

        private readonly DbContextBroker context;
        public AccountRepository(DbContextBroker _context) {
            context = _context;
        }
        
        public async Task<IEnumerable<Account>> GetAllAccounts() {
            return await context.Accounts.ToListAsync();
        }
        public async Task<Account> GetAccount(int _accountId = 1) {
            return await context.FindAsync<Account>(_accountId);
        }
        
        public async Task<Account> PostAccount(Account _account) {
            await context.Accounts.AddAsync(_account);
            await context.SaveChangesAsync();
            return null;
        }
        public void DeleteAccount(int _accountId) {
            context.Accounts.Remove(GetAccount(_accountId).Result);
            context.SaveChanges();
        }
    }
}
