using System.ComponentModel.DataAnnotations;
using Models_Mir2_V2_WebApi;
using SharedModels_Mir2_V2.BaseModels;
namespace Database_Mir2_V2_WebApi.Attributes {
    public class UniqueEmailAttribute : ValidationAttribute {
        public IDataAccessService<Account> AccountService;

        public override bool IsValid(object value) {
            if (value == null)
                return true;
            return AccountService.GetAccountByEmail(value.ToString()) == null;
        }
    }
}
