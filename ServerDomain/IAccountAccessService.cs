using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.Enums;

namespace Models_Mir2_V2_WebApi {
    public interface IAccountAccessService<T> {
        Task<IEnumerable<T>> GetAllAccounts();
        Task<AccountLoginResult> GetAccount(AccountLoginDtoC2S accountDto);
        Task<AccountRegisterResult> PostAccount(AccountRegisterDtoC2S accountDbEntry);
        void DeleteAccount(int accountId);
        public AccountRegisterResult IsEmailOrUserNameAlreadyRegistered(string email, string userName);
    }
}
