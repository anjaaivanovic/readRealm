using System.IdentityModel.Tokens.Jwt;

namespace ReadRealmBackend.Common
{
    public class JwtHelper
    {
        private readonly string _secretKey;
        private readonly ConfigProvider _configProvider;

        public JwtHelper(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            _secretKey = configProvider.JwtKey;
        }

        public string GetUserIdFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is null or empty.", nameof(token));
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId");
                return userIdClaim?.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid token", ex);
            }
        }
    }
}