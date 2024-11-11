using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Security.Tokens;
using Moq;

namespace Common.Tests.Utilities.Tokens;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock
            .Setup(tokenGenerator => tokenGenerator.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserType>(), It.IsAny<bool>()))
            .Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        return mock.Object;
    }
}
