﻿using AutoMapper;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Authors;
using ReadRealmBackend.Models.Requests.BookAuthors;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Requests.BookTypes;
using ReadRealmBackend.Models.Requests.BookUsers;
using ReadRealmBackend.Models.Requests.Genres;
using ReadRealmBackend.Models.Requests.Languages;
using ReadRealmBackend.Models.Requests.Notes;
using ReadRealmBackend.Models.Requests.NoteTypes;
using ReadRealmBackend.Models.Requests.Statuses;
using ReadRealmBackend.Models.Responses.Authors;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.BookTypes;
using ReadRealmBackend.Models.Responses.Generic;
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
            #region Book

            CreateMap<Book, BookResponse>()
                 .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages.Select(l => l.Name).ToList()))
                .ReverseMap();
            CreateMap<Book, InsertBookRequest>()
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Published.ToDateTime(new TimeOnly(0, 0))))
                .ReverseMap()
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Published)));
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
                 .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                 .ReverseMap();
            CreateMap<GenericPaginationResponse<Book>, GenericPaginationResponse<RecommendedBookResponse>>().ReverseMap();

            CreateMap<Book, RecommendedBookByFriendsActivityResponse>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.BookUsers.FirstOrDefault().Rating))
                .ForMember(dest => dest.FriendQuote, opt => opt.MapFrom(src => src.Notes.OrderByDescending(n => n.DatePosted).FirstOrDefault().Text))
                .ForMember(dest => dest.Friend, opt => opt.MapFrom(src => src.BookUsers.FirstOrDefault().UserId))
                .ReverseMap();

            CreateMap<UsersBook, UsersBookResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Book.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
               .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Book.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
               .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.Book.Isbn))
               .ForMember(dest => dest.CurrentChapter, opt => opt.MapFrom(src => src.BookUser.CurrentChapter))
               .ForMember(dest => dest.ChapterCount, opt => opt.MapFrom(src => src.Book.ChapterCount))
               .ReverseMap();
            CreateMap<GenericPaginationResponse<UsersBook>, GenericPaginationResponse<UsersBookResponse>>().ReverseMap();

            CreateMap<Book, MutualBookResponse>()
                 .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()))
                 .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.BookUsers.Select(bu => bu.UserId).ToList()))
                 .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
                 .ReverseMap();
            CreateMap<GenericPaginationResponse<Book>, GenericPaginationResponse<MutualBookResponse>>().ReverseMap();

            #endregion

            #region Note

            CreateMap<Note, NoteResponse>()
              .ForMember(dest => dest.Visibility, opt => opt.MapFrom(src => src.NoteVisibility.Name))
              .ReverseMap();
            CreateMap<GenericPaginationResponse<Note>, GenericPaginationResponse<NoteResponse>>().ReverseMap();
            CreateMap<InsertNoteRequest, InsertNoteFullRequest>().ReverseMap();
            CreateMap<Note, InsertNoteFullRequest>()
                .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => src.DatePosted.ToDateTime(new TimeOnly(0, 0))))
                .ReverseMap()
                .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DatePosted)));

            #endregion

            #region Author

            CreateMap<Author, InsertAuthorRequest>().ReverseMap();
            CreateMap<Author, UpdateAuthorRequest>().ReverseMap();
            CreateMap<Author, AuthorResponse>().ReverseMap();

            #endregion

            #region BookType

            CreateMap<BookType, InsertBookTypeRequest>().ReverseMap();
            CreateMap<BookType, UpdateBookTypeRequest>().ReverseMap();
            CreateMap<BookType, BookTypeResponse>().ReverseMap();

            #endregion

            #region Genre

            CreateMap<Genre, InsertGenreRequest>().ReverseMap();
            CreateMap<Genre, UpdateGenreRequest>().ReverseMap();
            CreateMap<Genre, GenreResponse>().ReverseMap();

            #endregion

            #region Language

            CreateMap<Language, InsertLanguageRequest>().ReverseMap();
            CreateMap<Language, UpdateLanguageRequest>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();

            #endregion

            #region NoteType

            CreateMap<NoteType, InsertNoteTypeRequest>().ReverseMap();
            CreateMap<NoteType, UpdateNoteTypeRequest>().ReverseMap();
            CreateMap<NoteType, NoteTypeResponse>().ReverseMap();

            #endregion

            #region Status

            CreateMap<Status, InsertStatusRequest>().ReverseMap();
            CreateMap<Status, UpdateStatusRequest>().ReverseMap();
            CreateMap<Status, StatusResponse>().ReverseMap();

            #endregion

            #region BookUser

            CreateMap<BookUser, InsertBookUserFullRequest>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToDateTime(new TimeOnly(0, 0))))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue ? src.EndDate.Value.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null))
                .ReverseMap()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue ? DateOnly.FromDateTime(src.EndDate.Value) : (DateOnly?)null));
            CreateMap<InsertBookUserRequest, InsertBookUserFullRequest>().ReverseMap();

            #endregion

            #region Friend

            CreateMap<Friend, FriendRequest>()
                .ForMember(dest => dest.SenderUserId, opt => opt.MapFrom(src => src.FirstUserId))
                .ForMember(dest => dest.ReceiverUserId, opt => opt.MapFrom(src => src.SecondUserId))
                .ReverseMap();

            #endregion
        }
    }
}