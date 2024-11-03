using Microsoft.AspNetCore.Identity;
using Moq;

namespace Common.Tests.Utilities.Cryptography;
public class PasswordHasherBuilder<T> where T : class
{
    private readonly Mock<IPasswordHasher<T>> _passwordHasher;

    public PasswordHasherBuilder(T t)
    {
        _passwordHasher = new Mock<IPasswordHasher<T>>();

        _passwordHasher.Setup(passwordHasher => passwordHasher.HashPassword(t, It.IsAny<string>())).Returns("AHjlG7nkFrqw2nG");
    }

    public PasswordHasherBuilder()
    {
        _passwordHasher = new Mock<IPasswordHasher<T>>();

        _passwordHasher.Setup(passwordHasher => passwordHasher.HashPassword(It.IsAny<T>(), It.IsAny<string>())).Returns("AHjlG7nkFrqw2nG");
    }

    public PasswordHasherBuilder<T> VerifyHashedPassword(T t ,string password)
    {
        _passwordHasher.Setup(passwordHasher => passwordHasher.VerifyHashedPassword(t, password, It.IsAny<string>())).Returns(PasswordVerificationResult.Success);

        return this;
    }

    public IPasswordHasher<T> Build() => _passwordHasher.Object;
}
