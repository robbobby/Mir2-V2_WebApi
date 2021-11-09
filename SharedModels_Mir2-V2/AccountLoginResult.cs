using SharedModels_Mir2_V2.AccountDto.LoginDto;
namespace SharedModels_Mir2_V2 {
    public class AccountLoginResult {
        public LoginResult LoginResult { get; }
        
        public AccountLoginDtoS2C Account { get; }
        public AccountLoginResult(LoginResult loginResult, AccountLoginDtoS2C account) {
            LoginResult = loginResult;
            Account = account;
        }
    }

    public enum LoginResult {
        UserNameDoesNotExist,
        WrongPassword,
        AccountBanned,
        Success
    }
}
