using System;
using SharedModels_Mir2_V2.BaseModels;
namespace SharedModels_Mir2_V2.AccountDto {
    public class AccountRegisterDto : Account {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
