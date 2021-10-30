using System.Collections.Generic;
using System.Threading.Tasks;
using Models_Mir2_V2_WebApi.Model;
namespace Models_Mir2_V2_WebApi {
    public interface IDataAccess<T> {
        Task<IEnumerable<T>> GetAllAccounts();
        public Task<T> GetAccount(int _accountId = 1);
        public Task<T> PostAccount(T _account);
        void DeleteAccount(int _accountId);
    }
}
