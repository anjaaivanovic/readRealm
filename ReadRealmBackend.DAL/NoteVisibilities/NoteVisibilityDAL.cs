using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.NoteVisibilities
{
    public class NoteVisibilityDAL : BaseDAL<NoteVisibility>, INoteVisibilityDAL
    {
        public NoteVisibilityDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckNoteVisibilityAsync(int id)
        {
            return await _set.AnyAsync(nv => nv.Id == id);
        }
    }
}