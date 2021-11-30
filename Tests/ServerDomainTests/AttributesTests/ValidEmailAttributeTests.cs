using Models_Mir2_V2_WebApi.Attributes;
using Xunit;
namespace ServerDomainTests.AttributesTests {
    public class ValidEmailAttributeTests {
        private ValidEmailAttribute _sut;
        private readonly string _validEmailAddress = "ValidEmailAddress@a";
        private readonly string _invalidEmailAddress = "InvalidEmailAddress@";
        
        public ValidEmailAttributeTests() {
            _sut = new ValidEmailAttribute();
        }

        [Fact]
        public void IsValid_ReturnsTrue_IfEmailIsValid() {
            Assert.True(_sut.IsValid(_validEmailAddress));    
        }
        
        [Fact]
        public void IsValid_ReturnsFalse_IfEmailIsInvalid() {
            Assert.False(_sut.IsValid(_invalidEmailAddress));    
        }
    }
}
