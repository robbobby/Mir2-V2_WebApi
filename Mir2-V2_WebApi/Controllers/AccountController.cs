using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Application.Repository;
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

        private readonly IAccountAccessService<AccountDbEntry> _accountAccountAccessService;
        private readonly IMapper _mapper;

        public AccountController(IAccountAccessService<AccountDbEntry> accountAccountAccessService, IMapper mapper, IConfiguration configuration) {
            _accountAccountAccessService = accountAccountAccessService;
            _mapper = mapper;
            Hashing.Pepper = configuration.GetValue<string>("pepper");
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<AccountDbEntry>> GetAllAccounts(int accountId = 1) {
            return await _accountAccountAccessService.GetAllAccounts();
        } 
        
        [HttpPost("[action]")]
        public async Task<AccountLoginResult> GetAccount([FromBody] AccountLoginDtoC2S account) {
            AccountLoginResult result = await _accountAccountAccessService.GetAccount(account);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<AccountRegisterResult> RegisterNewAccount([FromBody] AccountRegisterDtoC2S accountDbEntry) {
            AccountRegisterResult accountResponse = await _accountAccountAccessService.PostAccount(accountDbEntry);
            return accountResponse;
        }

        [HttpPost("[action]")]
        public async Task DeleteAccount(int accountId) {
            _accountAccountAccessService.DeleteAccount(accountId);
        }
    }
}