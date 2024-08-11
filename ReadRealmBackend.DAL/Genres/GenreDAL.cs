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

        public async Task<bool> CheckGenreAsync(int id)
        {
            return await _set.AnyAsync(genre => genre.Id == id);
        }

        public async Task<bool> CheckGenreByNameAsync(string name)
        {
            return await _set.AnyAsync(genre => genre.Name== name);
        }
    }
}