using System;
using SharedModels_Mir2_V2.BaseModels;
namespace SharedModels_Mir2_V2.AccountDto {
    public class AccountRegisterDtoC2S : Account {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public AccountRegisterDtoC2S(string _firstName, string _lastName, string _userName, string _password, string _email) {
            FirstName = _firstName;
            LastName = _lastName;
            UserName = _userName;
            Password = _password;
            Email = _email;
        }
        public AccountRegisterDtoC2S() {
        }
    }
}
