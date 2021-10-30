using System.Collections.Generic;
using System.Threading.Tasks;
using Models_Mir2_V2_WebApi.Model;
namespace Models_Mir2_V2_WebApi {
    public interface IDataAccess {
        Task<IEnumerable<Account>> GetAllAccounts();
        public Task<Account> GetAccount(int _accountId = 1);
        public Task<Account> PostAccount(Account _account);
        void DeleteAccount(int _accountId);
    }
}
