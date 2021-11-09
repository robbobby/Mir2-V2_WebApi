using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mir2_V2_WebApi.Helpers;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.Enums;
namespace Mir2_v2_WebApi.Controllers {

    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class AccountController : ControllerBase {

        private readonly IDataAccessService<AccountDbEntry> _accountDataAccessService;
        private readonly IMapper _mapper;
        private readonly string _pepper;

        public AccountController(IDataAccessService<AccountDbEntry> accountDataAccessService, IMapper mapper, IConfiguration configuration) {
            _accountDataAccessService = accountDataAccessService;
            _mapper = mapper;
            _pepper = configuration.GetValue<string>("pepper");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("[action]")]
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts(int accountId = 1) {
            return await _accountDataAccessService.GetAllAccounts();
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("[action]")]
        public async Task<AccountDbEntry> GetAccount(int accountId = 1) {
            AccountDbEntry x = await _accountDataAccessService.GetAccount(accountId);
            Log.Error(x.Email);
            Log.Error(x.Password);
            return x;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("[action]")]
        public async Task<AccountRegisterResult> RegisterNewAccount([Microsoft.AspNetCore.Mvc.FromBody] AccountRegisterDtoC2S accountDbEntry) {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.InternalServerError;
            if (!new EmailAddressAttribute().IsValid(accountDbEntry.Email))
                return AccountRegisterResult.EmailNotValid;
            if (_accountDataAccessService.IsEmailAlreadyRegistered(accountDbEntry.Email))
                return AccountRegisterResult.EmailAlreadyExists;

            string salt = Hashing.GetRandomSalt();
            accountDbEntry.Password = Hashing.HashPassword($"{accountDbEntry.Password}{_pepper}", salt);
            
            AccountDbEntry account = _mapper.Map<AccountDbEntry>(accountDbEntry);
            account.Salt = salt;

            var accountResponse = await _accountDataAccessService.PostAccount(account);
            return AccountRegisterResult.Success;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("[action]")]
        public async Task DeleteAccount(int accountId) {
            _accountDataAccessService.DeleteAccount(accountId);
        }
    }
}
