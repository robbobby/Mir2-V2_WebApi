using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using Xunit;
namespace ServerDomainTests.ModelsTests {
    public class CharacterDbEntryTests {

        [Fact]
        public void Id_HasKeyAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("Id");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(KeyAttribute)));
        }
    
        [Fact]
        public void Account_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("Account");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
            
        [Fact]
        public void Name_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("Name");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
            
        [Fact]
        public void CharacterClass_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("CharacterClass");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
            
        [Fact]
        public void Gender_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("Gender");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
        
            
        [Fact]
        public void Level_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("Level");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
        
            
        [Fact]
        public void Experience_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("Experience");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
        
            
        [Fact]
        public void IsDeleted_HasRequiredAttribute() {
            PropertyInfo? property = typeof(CharacterDbEntry).GetProperty("IsDeleted");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(RequiredAttribute)));
        }
    }
}
