using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberBoss.Infrastructure.Security.Tokens;
public class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expireInMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expireInMinutes, string signingKey)
    {
        _expireInMinutes = expireInMinutes;
        _signingKey = signingKey;
    }

    public string GenerateToken(Guid id, string name, string email, UserType userType)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, id.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, userType.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expireInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);                
    }
}
