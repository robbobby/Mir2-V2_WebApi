using System;
namespace SharedModels_Mir2_V2.AccountDto.LoginDto {
    public class AccountLoginDtoS2C {
        public int Id { get; set; }
        private Guid SessionToken { get; set; }
    }
}
