using ReadRealmBackend.Models.Responses.NoteTypes;
using ReadRealmBackend.Models.Requests.NoteTypes;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.NoteTypes
{
    public interface INoteTypeBL
    {
        Task<GenericResponse<NoteTypeResponse>> GetNoteTypeAsync(int id);
        Task<GenericResponse<List<NoteTypeResponse>>> GetNoteTypesAsync();
        Task<GenericResponse<string>> InsertNoteTypeAsync(InsertNoteTypeRequest req);
        Task<GenericResponse<string>> UpdateNoteTypeAsync(UpdateNoteTypeRequest req);
        Task<GenericResponse<string>> DeleteNoteTypeAsync(int id);
    }
}