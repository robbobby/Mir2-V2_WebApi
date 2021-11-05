using System;
using System.ComponentModel.DataAnnotations;
namespace Models_Mir2_V2_WebApi.Attributes {
    public class ValidEmailAttribute : ValidationAttribute {
        public override bool IsValid(object _value) {
            return new EmailAddressAttribute().IsValid(_value.ToString());
        }
    }
}
