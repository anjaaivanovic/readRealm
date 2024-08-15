using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadRealmBackend.BL.Authors;
using ReadRealmBackend.BL.Books;
using ReadRealmBackend.BL.BookTypes;
using ReadRealmBackend.BL.BookUsers;
using ReadRealmBackend.BL.Genres;
using ReadRealmBackend.BL.Home;
using ReadRealmBackend.BL.Languages;
using ReadRealmBackend.BL.NoteTypes;
using ReadRealmBackend.BL.Statuses;
using ReadRealmBackend.Common.Services;
using ReadRealmBackend.DAL.Authors;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.DAL.BookTypes;
using ReadRealmBackend.DAL.BookUsers;
using ReadRealmBackend.DAL.Friends;
using ReadRealmBackend.DAL.Genres;
using ReadRealmBackend.DAL.Languages;
using ReadRealmBackend.DAL.NoteTypes;
using ReadRealmBackend.DAL.Statuses;
using ReadRealmBackend.Models.Context;

namespace ReadRealmBackend.Common
{
    public class ServiceInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            InitializeDAL(services);
            InitializeBL(services);
            InitializeOther(services);
        }

        public void InitializeDAL(IServiceCollection services)
        {
            services.AddDbContext<ReadRealmContext>(options =>
                options.UseSqlServer(ConfigProvider.ConnectionString)
            );
            services.AddScoped<IAuthorDAL, AuthorDAL>();
            services.AddScoped<IBookDAL, BookDAL>();
            services.AddScoped<IBookTypeDAL, BookTypeDAL>();
            services.AddScoped<IGenreDAL, GenreDAL>();
            services.AddScoped<ILanguageDAL, LanguageDAL>();
            services.AddScoped<INoteTypeDAL, NoteTypeDAL>();
            services.AddScoped<IStatusDAL, StatusDAL>();
            services.AddScoped<IBookUserDAL, BookUserDAL>();
            services.AddScoped<IFriendDAL, FriendDAL>();
        }

        public void InitializeBL(IServiceCollection services)
        {
            services.AddScoped<IAuthorBL, AuthorBL>();
            services.AddScoped<IBookBL, BookBL>();
            services.AddScoped<IBookTypeBL, BookTypeBL>();
            services.AddScoped<IGenreBL, GenreBL>();
            services.AddScoped<ILanguageBL, LanguageBL>();
            services.AddScoped<INoteTypeBL, NoteTypeBL>();
            services.AddScoped<IStatusBL, StatusBL>();
            services.AddScoped<IHomeBL, HomeBL>();
            services.AddScoped<IBookUserBL, BookUserBL>();
        }

        public void InitializeOther(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}