using System.Collections.Generic;
using System.Threading.Tasks;
using Models_Mir2_V2_WebApi.Model;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.BaseModels;
using SharedModels_Mir2_V2.Enums;
namespace Models_Mir2_V2_WebApi {
    public interface IDataAccessService<T> {
        Task<IEnumerable<T>> GetAllAccounts();
        public Task<AccountLoginResult> GetAccount(AccountLoginDtoC2S accountDto);
        public Task<T> PostAccount(T account);
        void DeleteAccount(int accountId);
        public AccountRegisterResult IsEmailOrUserNameAlreadyRegistered(string email, string userName);
    }
}
