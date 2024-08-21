using AutoMapper;
using ReadRealmBackend.DAL.Genres;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Genres;
using ReadRealmBackend.Models.Responses.Generic;
using ReadRealmBackend.Models.Responses.Genres;

namespace ReadRealmBackend.BL.Genres
{
    public class GenreBL: IGenreBL
    {
        private readonly IGenreDAL _genreDAL;
        private readonly IMapper _mapper;

        public GenreBL(IGenreDAL genreDAL, IMapper mapper)
        {
            _genreDAL = genreDAL;
            _mapper = mapper;
        }

        #region Get

        public async Task<GenericResponse<GenreResponse>> GetGenreAsync(int id)
        {
            var genre = await _genreDAL.GetOneAsync(id);

            if (genre== null)
            {
                return new GenericResponse<GenreResponse>
                {
                    Success = false,
                    Errors = new List<string> { "No genre with such id!" }
                };
            }

            return new GenericResponse<GenreResponse>
            {
                Success = true,
                Data = _mapper.Map<Genre, GenreResponse>(genre)
            };
        }

        public async Task<GenericResponse<List<GenreResponse>>> GetGenresAsync()
        {
            var genres = await _genreDAL.GetAllAsync();

            return new GenericResponse<List<GenreResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<Genre>, List<GenreResponse>>(genres)
            };
        }

        #endregion

        #region Insert

        public async Task<GenericResponse<string>> InsertGenreAsync(InsertGenreRequest req)
        {
            if (await _genreDAL.CheckGenreByNameAsync(req.Name))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Genre already exists!" }
                };
            }

            await _genreDAL.InsertOneAsync(_mapper.Map<InsertGenreRequest, Genre>(req));
            var success = await _genreDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted genre!"
                };
            }
            else
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Errors = new List<string> { "Changes could not be saved!" }
                };
            }
        }

        #endregion

        #region Update

        public async Task<GenericResponse<string>> UpdateGenreAsync(UpdateGenreRequest req)
        {
            if (!await _genreDAL.CheckGenreAsync(req.Id))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No genre with such id!" }
                };
            }

            _genreDAL.UpdateOne(_mapper.Map<UpdateGenreRequest, Genre>(req));
            var success = await _genreDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated genre!"
                };
            }
            else
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Errors = new List<string> { "Changes could not be saved!" }
                };
            }
        }

        #endregion

        #region Delete

        public async Task<GenericResponse<string>> DeleteGenreAsync(int id)
        {
            var toDelete = await _genreDAL.GetOneAsync(id);

            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No genre with such id!" }
                };
            }

            _genreDAL.DeleteOne(toDelete);
            var success = await _genreDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted genre!"
                };
            }
            else
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Errors = new List<string> { "Changes could not be saved!" }
                };
            }
        }

        #endregion
    }
}