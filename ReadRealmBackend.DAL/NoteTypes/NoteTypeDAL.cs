using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.NoteTypes
{
    public class NoteTypeDAL : BaseDAL<NoteType>, INoteTypeDAL
    {
        public NoteTypeDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckNoteTypeAsync(int id)
        {
            return await _set.AnyAsync(noteType => noteType.Id == id);
        }

        public async Task<bool> CheckNoteTypeByNameAsync(string name)
        {
            return await _set.AnyAsync(noteType => noteType.Name == name);
        }
    }
}