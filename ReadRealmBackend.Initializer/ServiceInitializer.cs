using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadRealmBackend.BL.Books;
using ReadRealmBackend.DAL.Books;
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
            services.AddScoped<IBookDAL, BookDAL>();
        }

        public void InitializeBL(IServiceCollection services)
        {
            services.AddScoped<IBookBL, BookBL>();
        }

        public void InitializeOther(IServiceCollection services)
        {
            services.AddSingleton<JwtHelper>();
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}