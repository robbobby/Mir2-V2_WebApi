using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
namespace SharedModels_Mir2_V2 {
    public class AccountLoginResult {
        public LoginResult LoginResult { get; }
        
        public AccountLoginDto Account { get; }
        public AccountLoginResult(LoginResult loginResult, AccountLoginDto account = null) {
            LoginResult = loginResult;
            Account = account;
        }
    }

    public enum LoginResult {
        UserNameDoesNotExist,
        WrongPassword,
        AccountBanned,
        Success,
        AlreadyLoggedIn,
        NetworkError
    }
}
