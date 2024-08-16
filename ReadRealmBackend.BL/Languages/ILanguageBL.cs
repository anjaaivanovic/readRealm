using ReadRealmBackend.Models.Responses.Languages;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Requests.Languages;

namespace ReadRealmBackend.BL.Languages
{
    public interface ILanguageBL
    {
        Task<GenericResponse<LanguageResponse>> GetLanguageAsync(int id);
        Task<GenericResponse<List<LanguageResponse>>> GetLanguagesAsync();
        Task<GenericResponse<string>> InsertLanguageAsync(InsertLanguageRequest req);
        Task<GenericResponse<string>> UpdateLanguageAsync(UpdateLanguageRequest req);
        Task<GenericResponse<string>> DeleteLanguageAsync(int id);
    }
}