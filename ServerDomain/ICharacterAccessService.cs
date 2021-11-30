using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using SharedModels_Mir2_V2.Enums;

namespace Models_Mir2_V2_WebApi {
    public interface ICharacterAccessService<T> {
        Task<IEnumerable<T>> GetAccountCharacters(int accountId, Guid accountSessionToken);
        Task<CharacterDbEntry> GetCharacter(int characterId);
        Task<CharacterRepositoryResult> PostCharacter(CharacterDbEntry characterDbEntry);
        Task<CharacterDeleteResult> DeleteCharacter(int characterId);
    }
}
