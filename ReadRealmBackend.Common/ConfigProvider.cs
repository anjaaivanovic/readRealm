using Microsoft.Extensions.Configuration;

namespace ReadRealmBackend.Common
{
    public class ConfigProvider
    {
        public string ConnectionString { get; }
        public string FrontendUrl { get; }
        public string JwtKey { get; }

        public ConfigProvider(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Default") ?? string.Empty;
            FrontendUrl = configuration.GetSection("FrontendUrl").Value ?? string.Empty;
            JwtKey = configuration.GetSection("Jwt:Key").Value ?? string.Empty;
        }
    }
}
