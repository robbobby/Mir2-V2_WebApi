using System;
namespace SharedModels_Mir2_V2.AccountDto.LoginDto {
    public class AccountLoginDtoS2C {
        public int Id { get; set; }
        public Guid SessionToken { get; set; }
        public AccountLoginDtoS2C(int id, Guid sessionToken) {
            Id = id;
            SessionToken = sessionToken;
        }
    }
}
