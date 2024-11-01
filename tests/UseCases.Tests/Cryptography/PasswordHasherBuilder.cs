using Microsoft.AspNetCore.Identity;
using Moq;

namespace UseCases.Tests.Cryptography;
public class PasswordHasherBuilder<T> where T : class
{
    public static IPasswordHasher<T> Build()
    {
        var mock = new Mock<IPasswordHasher<T>>();

        mock.Setup(passwordHasher => passwordHasher.HashPassword(It.IsAny<T>(), It.IsAny<string>())).Returns("AHjlG7nkFrqw2nG");

        return mock.Object;
    }
}
