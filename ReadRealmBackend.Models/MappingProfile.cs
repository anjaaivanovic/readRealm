using AutoMapper;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Notes;

namespace ReadRealmBackend.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookResponse>()
                 .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName).ToList()))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages.Select(l => l.Name).ToList()))
                .ReverseMap();

            CreateMap<Note, NoteResponse>().ReverseMap();
        }
    }
}
