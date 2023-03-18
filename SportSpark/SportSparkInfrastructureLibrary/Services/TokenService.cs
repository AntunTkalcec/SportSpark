using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkInfrastructureLibrary.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportSparkInfrastructureLibrary.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenDataConfiguration _tokenDataConfiguration;

        public TokenService(IOptions<TokenDataConfiguration> tokenDataConfiguration)
        {
            _tokenDataConfiguration = tokenDataConfiguration.Value;
        }

        public string GenerateJwt(List<Claim> claims, int expirationInMinutes)
        {
            var now = DateTime.UtcNow;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenDataConfiguration.SecretKey));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _tokenDataConfiguration.Issuer,
                audience: _tokenDataConfiguration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(expirationInMinutes),
                signingCredentials: signInCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public List<Claim> GetClaimsFromJwt(string jwt)
        {
            try
            {
                JwtSecurityTokenHandler handler = new();

                handler.ValidateToken(jwt, TokenValidationConfiguration.GetTokenValidationParameters(_tokenDataConfiguration.Issuer,
                    _tokenDataConfiguration.Audience, _tokenDataConfiguration.SecretKey), out SecurityToken securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    return jwtSecurityToken?.Claims?.ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
