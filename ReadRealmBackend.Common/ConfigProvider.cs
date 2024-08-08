using Microsoft.Extensions.Configuration;

namespace ReadRealmBackend.Common
{
    public static class ConfigProvider
    {
        public static string ConnectionString { get; private set; } = string.Empty;
        public static string FrontendUrl { get; private set; } = string.Empty;
        public static string JwtKey { get; private set; } = string.Empty;
        public static string AllowedOrigins { get; private set; } = string.Empty;
        public static string Issuer { get; private set; } = string.Empty;
        public static string Audience { get; private set; } = string.Empty;

        public static void Initialize(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Default") ?? string.Empty;
            FrontendUrl = configuration.GetSection("FrontendUrl").Value ?? string.Empty;
            JwtKey = configuration.GetSection("Jwt:Key").Value ?? string.Empty;
            Issuer = configuration.GetSection("Jwt:Issuer").Value ?? string.Empty;
            Audience = configuration.GetSection("Jwt:Audience").Value ?? string.Empty;
            AllowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Value ?? string.Empty;
            
        }
    }
}
