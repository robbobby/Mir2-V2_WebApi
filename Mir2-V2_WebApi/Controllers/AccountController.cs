using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
namespace Mir2_v2_WebApi.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class AccountController {

        private readonly IDataAccess accountDataAccess;


        public AccountController(IDataAccess _accountDataAccess) {
            accountDataAccess = _accountDataAccess;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Account>> GetAllAccounts(int _accountId = 1) {
            return await accountDataAccess.GetAllAccounts();
        }


        [HttpGet("[action]")]
        public async Task<Account> GetAccount(int _accountId = 1) {
            return await accountDataAccess.GetAccount(_accountId);
        }

        [HttpPost("[action]")]
        public async Task PostAccount(Account _account) {
            await accountDataAccess.PostAccount(_account);
        }

        [HttpPost("[action]")]
        public async Task DeleteAccount(int _accountId) {
            accountDataAccess.DeleteAccount(_accountId);
        }
    }
}
