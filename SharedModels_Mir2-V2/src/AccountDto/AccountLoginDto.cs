using System;
namespace SharedModels_Mir2_V2.AccountDto {
    public class AccountLoginDto {
        public int Id { get; set; }
        public Guid SessionToken { get; set; }
        public AccountLoginDto(int id, Guid sessionToken) {
            Id = id;
            SessionToken = sessionToken;
        }
        public AccountLoginDto() {
        }
    }
}
