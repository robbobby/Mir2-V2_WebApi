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

        private readonly IDataAccessService<AccountDbEntry> accountDataAccessService;
        private readonly IMapper mapper;
        private readonly string pepper;

        public AccountController(IDataAccessService<AccountDbEntry> _accountDataAccessService, IMapper _mapper, IConfiguration _configuration) {
            accountDataAccessService = _accountDataAccessService;
            mapper = _mapper;
            pepper = _configuration.GetValue<string>("pepper");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("[action]")]
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts(int _accountId = 1) {
            return await accountDataAccessService.GetAllAccounts();
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("[action]")]
        public async Task<AccountDbEntry> GetAccount(int _accountId = 1) {
            AccountDbEntry x = await accountDataAccessService.GetAccount(_accountId);
            Log.Error(x.Email);
            Log.Error(x.Password);
            return x;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("[action]")]
        public async Task<AccountRegisterResult> RegisterNewAccount([Microsoft.AspNetCore.Mvc.FromBody] AccountRegisterDtoC2S _accountDbEntry) {
            Log.Debug(_accountDbEntry.Password);
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.InternalServerError;
            if (!new EmailAddressAttribute().IsValid(_accountDbEntry.Email))
                return AccountRegisterResult.EmailNotValid;
            if (accountDataAccessService.IsEmailAlreadyRegistered(_accountDbEntry.Email))
                return AccountRegisterResult.EmailAlreadyExists;

            string salt = Hashing.GetRandomSalt();
            _accountDbEntry.Password = Hashing.HashPassword($"{_accountDbEntry.Password}{pepper}", salt);
            
            AccountDbEntry account = mapper.Map<AccountDbEntry>(_accountDbEntry);
            account.Salt = salt;

            var accountResponse = await accountDataAccessService.PostAccount(account);
            return AccountRegisterResult.Success;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("[action]")]
        public async Task DeleteAccount(int _accountId) {
            accountDataAccessService.DeleteAccount(_accountId);
        }
    }
}
