using Microsoft.Extensions.DependencyInjection;

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
            
        }

        public void InitializeBL(IServiceCollection services)
        {

        }

        public void InitializeOther(IServiceCollection services)
        {
            services.AddSingleton<ConfigProvider>();
            services.AddSingleton<JwtHelper>();
        }
    }
}