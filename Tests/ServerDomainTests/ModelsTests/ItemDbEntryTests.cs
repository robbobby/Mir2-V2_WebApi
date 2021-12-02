using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using Xunit;
namespace ServerDomainTests.ModelsTests {
    public class ItemDbEntryTests {
        
        [Fact]
        public void Id_HasKeyAttribute() {
            PropertyInfo? property = typeof(ItemDbEntry).GetProperty("Id");
            Assert.NotNull(property);
            Assert.True(Attribute.IsDefined(property, typeof(KeyAttribute)));
        }
    }
}
