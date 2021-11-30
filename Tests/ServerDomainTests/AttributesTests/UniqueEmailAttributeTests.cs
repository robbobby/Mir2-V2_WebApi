using Castle.Core.Logging;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Attributes;
using Moq;
using SharedModels_Mir2_V2.BaseModels;
using SharedModels_Mir2_V2.Enums;
using Xunit;

namespace ServerDomainTests {
    public class UniqueEmailAttributeTests {
        private readonly UniqueEmailAttribute _sut;
        private readonly Mock<IAccountAccessService<Account>> _accountAccessServiceMock = new();
        private readonly string _validEmailAddress = "ValidEmailAddress@a";
        private readonly string _invalidEmailAddress = "InvalidEmailAddress@";

        public UniqueEmailAttributeTests() {
            _sut = new UniqueEmailAttribute {
                AccountService = _accountAccessServiceMock.Object
            };
            _accountAccessServiceMock.Setup(x =>
                x.IsEmailOrUserNameAlreadyRegistered(_validEmailAddress, "UserName")).Returns(AccountRegisterResult.Success);
        }

        [Fact]
        public void IsValidEmailAddressReturnsOk() {
            AccountRegisterResult accountRegisterResult = _sut.IsValid(_validEmailAddress, "UserName");
            Assert.Equal(AccountRegisterResult.Success, accountRegisterResult);
        }

        [Fact]
        public void IsValidReturnsEmailNotValid() {
            AccountRegisterResult accountRegisterResult = _sut.IsValid(_invalidEmailAddress, "UserName");
            Assert.Equal(AccountRegisterResult.EmailNotValid, accountRegisterResult);
        }

        [Fact]
        public void IsValid_Returns_IAccountAccessService_IsEmailOrUserNameAlreadyRegistered_Value() {
            AccountRegisterResult accountRegisterResult = _sut.IsValid(_validEmailAddress, "UserName");
            Assert.Equal(AccountRegisterResult.Success, accountRegisterResult);
        }

        [Fact(Skip = "Unsure how to test exceptions like this")]
        public void AccountServiceGetThrowsExceptionIfNull() {
            _sut.AccountService = null;
            Assert.Throws<LoggerException>(() => _sut.AccountServiceInjection);
        }
    }
}
