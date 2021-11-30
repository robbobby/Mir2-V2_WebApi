using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Models_Mir2_V2_WebApi.Attributes;
using Models_Mir2_V2_WebApi.Model;
using Xunit;
namespace ServerDomainTests.ModelsTests {
    public class AccountDbEntryTests {
        
        [Fact]
        public void Id_HasKeyAttribute() {
            PropertyInfo? property = typeof(AccountDbEntry).GetProperty("Id");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(KeyAttribute)));
        }

        [Fact]
        public void Password_HasRequiredAttribute() {
            PropertyInfo? property = typeof(AccountDbEntry).GetProperty("Password");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
        
        [Fact]
        public void Email_HasRequiredAttribute() {
            PropertyInfo? property = typeof(AccountDbEntry).GetProperty("Email");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
        
        [Fact]
        public void Email_HasUniqueEmailAttribute() {
            PropertyInfo? property = typeof(AccountDbEntry).GetProperty("Email");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(UniqueEmailAttribute)));
        }
        
        [Fact]
        public void UserName_HasRequiredAttribute() {
            PropertyInfo? property = typeof(AccountDbEntry).GetProperty("UserName");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
    }
}
