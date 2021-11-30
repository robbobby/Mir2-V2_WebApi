using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using SharedModels_Mir2_V2.AccountDto;
using SharedModels_Mir2_V2.AccountDto.LoginDto;
using SharedModels_Mir2_V2.Enums;
namespace Mir2_v2_WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase {
        private readonly ICharacterAccessService<CharacterDbEntry> _characterAccountAccessService;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterAccessService<CharacterDbEntry> characterAccountAccessService, IMapper mapper) {
            _characterAccountAccessService = characterAccountAccessService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IEnumerable<CharacterDbEntry>> GetAccountsCharacter([FromBody] AccountLoginDto account) {
            return await _characterAccountAccessService.GetAccountCharacters(account.Id, account.SessionToken);
        }
        
        [HttpPost("[action]")]
        public async Task<CharacterRepositoryResult> CreateNewCharacter([FromBody] CharacterRegisterDtoC2S characterRegister, [FromBody] int accountId) {
            CharacterDbEntry characterDbEntry = _mapper.Map<CharacterDbEntry>(characterRegister);
            characterDbEntry.Account.Id = accountId;
            return await _characterAccountAccessService.PostCharacter(characterDbEntry);
        }

        [HttpPost("[action]")]
        public async Task<CharacterDeleteResult> DeleteCharacter([FromBody] int accountId) {
            return await _characterAccountAccessService.DeleteCharacter(accountId);
        }
    }
}
