using System.Collections.Generic;
using System.Threading.Tasks;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
namespace Models_Mir2_V2_WebApi {
    public interface IDataAccessService<T> {
        Task<IEnumerable<T>> GetAllAccounts();
        public Task<AccountLoginResult> GetAccount(AccountLoginDtoC2S accountDto);
        public Task<T> PostAccount(T _account);
        void DeleteAccount(int _accountId);
        public bool IsEmailAlreadyRegistered(string _email);
    }
}
