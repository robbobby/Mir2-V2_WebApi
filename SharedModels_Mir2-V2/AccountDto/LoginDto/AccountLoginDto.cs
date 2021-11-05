using System;
namespace SharedModels_Mir2_V2.AccountDto {
    public class AccountLoginDto {
        public int Id { get; set; }
        private Guid SessionToken { get; set; }
    }
}
