using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Statuses
{
    public class StatusDAL : BaseDAL<Status>, IStatusDAL
    {
        public StatusDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckStatusAsync(int id)
        {
            return await _set.AnyAsync(status => status.Id == id);
        }

        public async Task<bool> CheckStatusByNameAsync(string name)
        {
            return await _set.AnyAsync(status => status.Name == name);
        }
    }
}