using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using UserManagement.Core.DTO;
using UserManagement.Core.IdentityEntities;
using UserManagement.Core.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserManagement.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateJwtToken(AppUser appUser)
        {
            var jwtToken = new JwtSecurityToken
         (
             claims: CreateClaims(appUser),
             expires: GetExpirationTime(),
             signingCredentials: GetSigningCredentials()
         );

            JwtSecurityTokenHandler tokenHandler = new();
            string token = tokenHandler.WriteToken(jwtToken);

            return new AuthenticationResponse
            {
                Username= appUser.UserName,
                Expiration = GetExpirationTime(),
                Token = token
            };
        
        }

        private DateTime GetExpirationTime()
        {
            var expirationMinutes = Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"]);
            return DateTime.UtcNow.AddMinutes(expirationMinutes);
        }

        private List<Claim> CreateClaims(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss,DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, GetExpirationTime().ToString())
            };
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration["Jwt:key"]?? "temp key";
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));

            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256Signature);

            return signingCredentials;

        }

  

    }
}
