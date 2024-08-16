using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Authors
{
    public class AuthorDAL : BaseDAL<Author>, IAuthorDAL
    {
        public AuthorDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckAuthorAsync(int id)
        {
            return await _set.AnyAsync(author =>  author.Id == id);
        }

        #region Check

        public async Task<bool> CheckAuthorByFullNameAsync(string fullName)
        {
            return await _set.AnyAsync(author => author.FirstName + " " + author.LastName == fullName);
        }

        #endregion

        #region Get

        public async Task<List<Author>> GetMultipleAuthorsAsync(List<int> ids)
        {
            return await _set.Where(author => ids.Contains(author.Id)).ToListAsync();
        }

        #endregion
    }
}