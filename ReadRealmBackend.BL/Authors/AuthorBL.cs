using AutoMapper;
using ReadRealmBackend.DAL.Authors;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Authors;
using ReadRealmBackend.Models.Responses.Authors;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.Authors
{
    public class AuthorBL : IAuthorBL
    {
        private readonly IAuthorDAL _authorDAL;
        private readonly IMapper _mapper;

        public AuthorBL(IAuthorDAL authorDAL, IMapper mapper)
        {
            _authorDAL = authorDAL;
            _mapper = mapper;
        }

        #region Get

        public async Task<GenericResponse<AuthorResponse>> GetAuthorAsync(int id)
        {
            var author = await _authorDAL.GetOneAsync(id);

            if (author == null)
            {
                return new GenericResponse<AuthorResponse>
                {
                    Success = false,
                    Errors = new List<string> { "No author with such id!" }
                };
            }

            return new GenericResponse<AuthorResponse>
            {
                Success = true,
                Data = _mapper.Map<Author, AuthorResponse>(author)
            };
        }

        public async Task<GenericResponse<List<AuthorResponse>>> GetAuthorsAsync()
        {
            var authors = await _authorDAL.GetAllAsync();

            return new GenericResponse<List<AuthorResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<Author>, List<AuthorResponse>>(authors)
            };
        }

        #endregion

        #region Insert

        public async Task<GenericResponse<string>> InsertAuthorAsync(InsertAuthorRequest req)
        {
            if (await _authorDAL.CheckAuthorByFullNameAsync(req.FirstName + " " + req.LastName))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Author already exists!" }
                };
            }

            await _authorDAL.InsertOneAsync(_mapper.Map<InsertAuthorRequest, Author>(req));
            var success = await _authorDAL.SaveAsync();

            if (success) 
            {
                return new GenericResponse<string> {
                    Success = success,
                    Data = "Successfully inserted author!"
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

        public async Task<GenericResponse<string>> UpdateAuthorAsync(UpdateAuthorRequest req)
        {           
            if (!await _authorDAL.CheckAuthorAsync(req.Id))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No author with such id!" }
                };
            }

            _authorDAL.UpdateOne(_mapper.Map<UpdateAuthorRequest, Author>(req));
            var success = await _authorDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated author!"
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

        public async Task<GenericResponse<string>> DeleteAuthorAsync(int id)
        {
            var toDelete = await _authorDAL.GetOneAsync(id);
            
            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No author with such id!" }
                };
            }

            _authorDAL.DeleteOne(toDelete);
            var success = await _authorDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted author!"
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