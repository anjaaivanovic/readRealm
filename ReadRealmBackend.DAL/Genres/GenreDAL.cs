using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Genres
{
    public class GenreDAL : BaseDAL<Genre>, IGenreDAL
    {
        public GenreDAL(ReadRealmContext context) : base(context)
        {
        }

        #region Check

        public async Task<bool> CheckGenreAsync(int id)
        {
            return await _set.AnyAsync(genre => genre.Id == id);
        }

        public async Task<bool> CheckGenreByNameAsync(string name)
        {
            return await _set.AnyAsync(genre => genre.Name== name);
        }

        #endregion


        #region Get

        public async Task<List<Genre>> GetMultipleGenresAsync(List<int> ids)
        {
            return await _set.Where(genre => ids.Contains(genre.Id)).ToListAsync();
        }

        #endregion
    }
}