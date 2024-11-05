﻿using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Services.LoggedUser;
using Moq;

namespace Common.Tests.Utilities.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Build(User? user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}
