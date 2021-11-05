using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Serilog;
using SharedModels_Mir2_V2.AccountDto;
namespace Mir2_v2_WebApi.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class AccountController {

        private readonly IDataAccessService<AccountDbEntry> accountDataAccessService;


        public AccountController(IDataAccessService<AccountDbEntry> _accountDataAccessService) {
            accountDataAccessService = _accountDataAccessService;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts(int _accountId = 1) {
            return await accountDataAccessService.GetAllAccounts();
        }


        [HttpGet("[action]")]
        public async Task<AccountDbEntry> GetAccount(int _accountId = 1) {
            AccountDbEntry x = await accountDataAccessService.GetAccount(_accountId);
            Log.Error(x.Email);
            Log.Error(x.Password);
            return x;
        }

        [HttpPost("[action]")]
        public async Task RegisterNewAccount([FromBody] AccountRegisterDtoC2S _accountDbEntry) {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.StatusCode = HttpStatusCode.InternalServerError;
            if  (! new EmailAddressAttribute().IsValid(_accountDbEntry.Email))
                return;
            AccountDbEntry account = new AccountDbEntry() {
                Email = _accountDbEntry.Email,
                FirstName = _accountDbEntry.FirstName,
                LastName = _accountDbEntry.LastName,
                Password = _accountDbEntry.Password,
                UserName = _accountDbEntry.UserName,
                SessionToken = Guid.Empty,
                IsLoggedIn = false
            };
            AccountDbEntry accountResponse = await accountDataAccessService.PostAccount(account);
        }


        [HttpPost("[action]")]
        public async Task DeleteAccount(int _accountId) {
            accountDataAccessService.DeleteAccount(_accountId);
        }
    }
}
