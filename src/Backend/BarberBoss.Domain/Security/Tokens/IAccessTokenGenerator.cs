using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string GenerateToken(Guid id, string name, string email, UserType userType);
}
