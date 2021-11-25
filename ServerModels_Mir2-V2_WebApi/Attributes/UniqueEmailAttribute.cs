using System;
using System.ComponentModel.DataAnnotations;
using SharedModels_Mir2_V2.BaseModels;
using SharedModels_Mir2_V2.Enums;
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

        public AccountRegisterResult IsValid(object emailValue, object userNameValue) {
            if (!new EmailAddressAttribute().IsValid(emailValue.ToString()))
                return AccountRegisterResult.EmailNotValid;
            return AccountService.IsEmailOrUserNameAlreadyRegistered(emailValue.ToString(), userNameValue.ToString());
        }
    }
}
