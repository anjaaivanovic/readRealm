using AutoMapper;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Authors;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Requests.BookTypes;
using ReadRealmBackend.Models.Requests.Genres;
using ReadRealmBackend.Models.Requests.Languages;
using ReadRealmBackend.Models.Requests.NoteTypes;
using ReadRealmBackend.Models.Requests.Statuses;
using ReadRealmBackend.Models.Responses.Authors;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.BookTypes;
using ReadRealmBackend.Models.Responses.Genres;
using ReadRealmBackend.Models.Responses.Languages;
using ReadRealmBackend.Models.Responses.Notes;
using ReadRealmBackend.Models.Responses.NoteTypes;
using ReadRealmBackend.Models.Responses.Statuses;

namespace ReadRealmBackend.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookResponse>()
                 .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages.Select(l => l.Name).ToList()))
                .ReverseMap();
            CreateMap<Book, InsertBookRequest>()
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Published.ToDateTime(new TimeOnly(0, 0))))
                .ReverseMap()
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Published)));

            CreateMap<Note, NoteResponse>().ReverseMap();

            CreateMap<Author, InsertAuthorRequest>().ReverseMap();
            CreateMap<Author, UpdateAuthorRequest>().ReverseMap();
            CreateMap<Author, AuthorResponse>().ReverseMap();

            CreateMap<BookType, InsertBookTypeRequest>().ReverseMap();
            CreateMap<BookType, UpdateBookTypeRequest>().ReverseMap();
            CreateMap<BookType, BookTypeResponse>().ReverseMap();

            CreateMap<Genre, InsertGenreRequest>().ReverseMap();
            CreateMap<Genre, UpdateGenreRequest>().ReverseMap();
            CreateMap<Genre, GenreResponse>().ReverseMap();

            CreateMap<Language, InsertLanguageRequest>().ReverseMap();
            CreateMap<Language, UpdateLanguageRequest>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();

            CreateMap<NoteType, InsertNoteTypeRequest>().ReverseMap();
            CreateMap<NoteType, UpdateNoteTypeRequest>().ReverseMap();
            CreateMap<NoteType, NoteTypeResponse>().ReverseMap();

            CreateMap<Status, InsertStatusRequest>().ReverseMap();
            CreateMap<Status, UpdateStatusRequest>().ReverseMap();
            CreateMap<Status, StatusResponse>().ReverseMap();

            CreateMap<Book, ContinueReadingBookResponse>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.CurrentChapter, opt => opt.MapFrom(src => src.BookUsers.FirstOrDefault().CurrentChapter))
                .ForMember(dest => dest.StartedOn, opt => opt.MapFrom(src => src.BookUsers.FirstOrDefault().StartDate))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.BookUsers.FirstOrDefault().Rating))
                .ForMember(dest => dest.TotalChapters, opt => opt.MapFrom(src => src.ChapterCount))
                .ReverseMap();

            CreateMap<Book, RecommendedBookResponse>()
                 .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                 .ReverseMap();

            CreateMap<Book, RecommendedBookByFriendsActivityResponse>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.BookUsers.FirstOrDefault().Rating))
                .ForMember(dest => dest.FriendQuote, opt => opt.MapFrom(src => src.Notes.OrderByDescending(n => n.DatePosted).FirstOrDefault().Text))
                .ReverseMap();
        }
    }
}