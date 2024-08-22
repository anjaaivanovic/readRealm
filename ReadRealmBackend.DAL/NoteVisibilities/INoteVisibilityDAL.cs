using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.NoteVisibilities
{
    public interface INoteVisibilityDAL: IBaseDAL<NoteVisibility>
    {
        Task<bool> CheckNoteVisibilityAsync(int id);
    }
}