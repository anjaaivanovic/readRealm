using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Languages
{
    public interface ILanguageDAL: IBaseDAL<Language>
    {
        Task<bool> CheckLanguageAsync(int id);
        Task<bool> CheckLanguageByNameAsync(string name);
        Task<List<Language>> GetMultipleLanguagesAsync(List<int> ids);
    }
}