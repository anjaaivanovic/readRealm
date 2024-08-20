using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Notes
{
    public class NoteDAL : BaseDAL<Note>, INoteDAL
    {
        public NoteDAL(ReadRealmContext context) : base(context)
        {
        }
    }
}