using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Genres
{
    public interface IGenreDAL: IBaseDAL<Genre>
    {
        Task<bool> CheckGenreAsync(int id);
        Task<bool> CheckGenreByNameAsync(string name);
        Task<List<Genre>> GetMultipleGenresAsync(List<int> ids);
    }
}