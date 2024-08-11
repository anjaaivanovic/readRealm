using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Languages
{
    public class LanguageDAL : BaseDAL<Language>, ILanguageDAL
    {
        public LanguageDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckLanguageAsync(int id)
        {
            return await _set.AnyAsync(language => language.Id == id);
        }

        public async Task<bool> CheckLanguageByNameAsync(string name)
        {
            return await _set.AnyAsync(language => language.Name == name);
        }
    }
}