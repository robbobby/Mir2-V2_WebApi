using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Database_Mir2_V2_WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;
using SharedModels_Mir2_V2;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.BaseModels;
using SharedModels_Mir2_V2.Enums;
namespace Mir2_v2_WebApi.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase {

        private readonly IDataAccessService<AccountDbEntry> _accountDataAccessService;
        private readonly IMapper _mapper;

        public AccountController(IDataAccessService<AccountDbEntry> accountDataAccessService, IMapper mapper, IConfiguration configuration) {
            _accountDataAccessService = accountDataAccessService;
            _mapper = mapper;
            Hashing.Pepper = configuration.GetValue<string>("pepper");
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts(int accountId = 1) {
            return await _accountDataAccessService.GetAllAccounts();
        }


        [HttpPost("[action]")]
        public async Task<AccountLoginResult> GetAccount([FromBody] AccountLoginDtoC2S account) {
            AccountLoginResult x = await _accountDataAccessService.GetAccount(account);
            return x;
        }

        [HttpPost("[action]")]
        public async Task<AccountRegisterResult> RegisterNewAccount([FromBody] AccountRegisterDtoC2S accountDbEntry) {
            HttpResponseMessage responseMessage = new();
            responseMessage.StatusCode = HttpStatusCode.InternalServerError;
            if (!new EmailAddressAttribute().IsValid(accountDbEntry.Email))
                return AccountRegisterResult.EmailNotValid;
            if (_accountDataAccessService.IsEmailAlreadyRegistered(accountDbEntry.Email))
                return AccountRegisterResult.EmailAlreadyExists;

            var salt = Hashing.GetRandomSalt();
            accountDbEntry.Password = Hashing.HashPassword(accountDbEntry.Password, salt);

            AccountDbEntry account = _mapper.Map<AccountDbEntry>(accountDbEntry);
            account.Salt = salt;

            AccountDbEntry accountResponse = await _accountDataAccessService.PostAccount(account);
            return AccountRegisterResult.Success;
        }


        [HttpPost("[action]")]
        public async Task DeleteAccount(int accountId) {
            _accountDataAccessService.DeleteAccount(accountId);
        }
    }
}
