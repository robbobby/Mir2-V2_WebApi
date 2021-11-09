using System;
using System.ComponentModel.DataAnnotations;
using SharedModels_Mir2_V2.BaseModels;
namespace Models_Mir2_V2_WebApi.Attributes {
    
    public class UniqueEmailAttribute : ValidationAttribute {

        public IDataAccessService<Account> AccountService { get; set; }

        public IDataAccessService<Account> AccountServiceInjection {
            get {
                if (AccountServiceInjection == null)
                    throw new Exception(("Account Access Service has not been set in Unique Email Attribute"));
                else
                    return AccountService;
            }
            set {
                AccountService = value;
            }
        }

        public override bool IsValid(object value) {
            if (!new EmailAddressAttribute().IsValid(value.ToString()))
                return false;
            return AccountService.IsEmailAlreadyRegistered(value.ToString());
        }
    }
}
