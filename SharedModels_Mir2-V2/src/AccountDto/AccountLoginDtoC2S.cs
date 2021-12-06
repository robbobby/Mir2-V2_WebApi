namespace SharedModels_Mir2_V2.AccountDto.LoginDto {
    public class AccountLoginDtoC2S {
        public AccountLoginDtoC2S(string userName, string password) {
            UserName = userName;
            Password = password;
        }
        public AccountLoginDtoC2S() {
            
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
