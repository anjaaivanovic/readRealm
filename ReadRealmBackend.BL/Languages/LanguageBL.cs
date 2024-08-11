using AutoMapper;
using ReadRealmBackend.DAL.Languages;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Languages;
using ReadRealmBackend.Models.Responses.Languages;

namespace ReadRealmBackend.BL.Languages
{
    public class LanguageBL: ILanguageBL
    {
        private readonly ILanguageDAL _languageDAL;
        private readonly IMapper _mapper;

        public LanguageBL(ILanguageDAL languageDAL, IMapper mapper)
        {
            _languageDAL = languageDAL;
            _mapper = mapper;
        }


        #region Get

        public async Task<GenericResponse<LanguageResponse>> GetLanguageAsync(int id)
        {
            var language = await _languageDAL.GetOneAsync(id);

            if (language == null)
            {
                return new GenericResponse<LanguageResponse>
                {
                    Success = false,
                    Errors = new List<string> { "No language with such id!" }
                };
            }

            return new GenericResponse<LanguageResponse>
            {
                Success = true,
                Data = _mapper.Map<Language, LanguageResponse>(language)
            };
        }

        public async Task<GenericResponse<List<LanguageResponse>>> GetLanguagesAsync()
        {
            var languages = await _languageDAL.GetAllAsync();

            return new GenericResponse<List<LanguageResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<Language>, List<LanguageResponse>>(languages)
            };
        }

        #endregion


        #region Insert

        public async Task<GenericResponse<string>> InsertLanguageAsync(InsertLanguageRequest req)
        {
            if (await _languageDAL.CheckLanguageByNameAsync(req.Name))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Language already exists!" }
                };
            }

            await _languageDAL.InsertOneAsync(_mapper.Map<InsertLanguageRequest, Language>(req));
            var success = await _languageDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted language!"
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

        public async Task<GenericResponse<string>> UpdateLanguageAsync(UpdateLanguageRequest req)
        {
            if (!await _languageDAL.CheckLanguageAsync(req.Id))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No language with such id!" }
                };
            }

            _languageDAL.UpdateOne(_mapper.Map<UpdateLanguageRequest, Language>(req));
            var success = await _languageDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated language!"
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

        public async Task<GenericResponse<string>> DeleteLanguageAsync(int id)
        {
            var toDelete = await _languageDAL.GetOneAsync(id);

            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No language with such id!" }
                };
            }

            _languageDAL.DeleteOne(toDelete);
            var success = await _languageDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted language!"
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