using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.NoteTypes
{
    public interface INoteTypeDAL: IBaseDAL<NoteType>
    {
        Task<bool> CheckNoteTypeAsync(int id);
        Task<bool> CheckNoteTypeByNameAsync(string name);
    }
}