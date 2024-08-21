using AutoMapper;
using ReadRealmBackend.DAL.NoteTypes;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.NoteTypes;
using ReadRealmBackend.Models.Responses.Generic;
using ReadRealmBackend.Models.Responses.NoteTypes;

namespace ReadRealmBackend.BL.NoteTypes
{
    public class NoteTypeBL: INoteTypeBL
    {
        private readonly INoteTypeDAL _noteTypeDAL;
        private readonly IMapper _mapper;

        public NoteTypeBL(INoteTypeDAL noteTypeDAL, IMapper mapper)
        {
            _noteTypeDAL = noteTypeDAL;
            _mapper = mapper;
        }

        #region Get

        public async Task<GenericResponse<NoteTypeResponse>> GetNoteTypeAsync(int id)
        {
            var noteType = await _noteTypeDAL.GetOneAsync(id);

            if (noteType == null)
            {
                return new GenericResponse<NoteTypeResponse>
                {
                    Success = false,
                    Errors = new List<string> { "No note type with such id!" }
                };
            }

            return new GenericResponse<NoteTypeResponse>
            {
                Success = true,
                Data = _mapper.Map<NoteType, NoteTypeResponse>(noteType)
            };
        }

        public async Task<GenericResponse<List<NoteTypeResponse>>> GetNoteTypesAsync()
        {
            var noteTypes = await _noteTypeDAL.GetAllAsync();

            return new GenericResponse<List<NoteTypeResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<NoteType>, List<NoteTypeResponse>>(noteTypes)
            };
        }

        #endregion

        #region Insert

        public async Task<GenericResponse<string>> InsertNoteTypeAsync(InsertNoteTypeRequest req)
        {
            if (await _noteTypeDAL.CheckNoteTypeByNameAsync(req.Name))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Note type already exists!" }
                };
            }

            await _noteTypeDAL.InsertOneAsync(_mapper.Map<InsertNoteTypeRequest, NoteType>(req));
            var success = await _noteTypeDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted note type!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        #endregion

        #region Update

        public async Task<GenericResponse<string>> UpdateNoteTypeAsync(UpdateNoteTypeRequest req)
        {
            if (!await _noteTypeDAL.CheckNoteTypeAsync(req.Id))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No note type with such id!" }
                };
            }

            _noteTypeDAL.UpdateOne(_mapper.Map<UpdateNoteTypeRequest, NoteType>(req));
            var success = await _noteTypeDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated note type!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        #endregion

        #region Delete

        public async Task<GenericResponse<string>> DeleteNoteTypeAsync(int id)
        {
            var toDelete = await _noteTypeDAL.GetOneAsync(id);

            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No note type with such id!" }
                };
            }

            _noteTypeDAL.DeleteOne(toDelete);
            var success = await _noteTypeDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted note type!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        #endregion
    }
}