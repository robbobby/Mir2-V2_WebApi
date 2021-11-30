using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository.Broker;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models_Mir2_V2_WebApi;
using Models_Mir2_V2_WebApi.Model;
using Models_Mir2_V2_WebApi.Models;
using SharedModels_Mir2_V2.Enums;
namespace Application.Repository.RepositoryService {
    public class CharacterRepository : ICharacterAccessService<CharacterDbEntry> {
        private readonly DbContextBroker _context;
        private IMapper _mapper;

        public CharacterRepository(DbContextBroker context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<CharacterDbEntry>> GetAccountCharacters(int accountId, Guid accountSessionToken) {
            if (await _context.Accounts.Where(account => account.Id == accountId).FirstAsync() != null)
                return await _context.Characters.Where(character => character.Account.Id == accountId).ToListAsync();
            // TODO: Needs a DTO to remove the account from the Character
            return null;
        }

        public async Task<CharacterDbEntry> GetCharacter(int characterId) {
            return await _context.Characters.Where(character => character.Id == characterId).FirstAsync();
        }
        
        public async Task<CharacterRepositoryResult> PostCharacter(CharacterDbEntry characterDbEntry) {
            if (await DoesCharacterNameExist(characterDbEntry.Name))
                return CharacterRepositoryResult.CharacterNameAlreadyExists;
            characterDbEntry.CreatedOn = DateTime.Now;
            characterDbEntry.IsDeleted = false;
            EntityEntry<CharacterDbEntry> x = await _context.Characters.AddAsync(characterDbEntry);
            if (x.Entity.Id != 0) {
                await _context.SaveChangesAsync();
                return CharacterRepositoryResult.Success;
            }
            return CharacterRepositoryResult.UnknownFailure;
        }
        
        public async Task<CharacterDeleteResult> DeleteCharacter(int characterId) {
            CharacterDbEntry character = await _context.Characters.Where(character => character.Id == characterId).FirstOrDefaultAsync();
            if (character.Id != characterId) {
                return CharacterDeleteResult.CharacterDoesNotExist;
            }
            if (character.IsDeleted)
                return CharacterDeleteResult.CharacterIsAlreadyDeleted;
            
            character.IsDeleted = true;
            int result = await _context.SaveChangesAsync();
            if (result ! > 0)
                return CharacterDeleteResult.FailedToDelete;
            return CharacterDeleteResult.Success;
        }

        private async Task<bool> DoesCharacterNameExist(string name) {
            return await _context.Characters.Where(character => character.Name == name).FirstAsync() != null;
        }
    }
}