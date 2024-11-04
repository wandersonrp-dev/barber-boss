using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberBoss.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly IDbContextFactory<BarberBossDbContext> _contextFactory;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(IDbContextFactory<BarberBossDbContext> contextFactory, ITokenProvider tokenProvider)
    {
        _contextFactory = contextFactory;
        _tokenProvider = tokenProvider;
    }

    public async Task<User?> Get()
    {
        var token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var claimUserType = jwtSecurityToken.Claims.First(claim => claim.Type == "role").Value;

        var claimIdentifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        var identifier = Guid.Parse(claimIdentifier);

        if (claimUserType is null)
            return null;

        Enum.TryParse<UserType>(claimUserType, out var userRole);

        using var context = await _contextFactory.CreateDbContextAsync();

        if (userRole == UserType.BarberShop)
        {
            return context.BarberShops
                .AsNoTracking()                
                .First(e => e.Id == identifier && e.UserStatus == UserStatus.Active);
        }
        else
            return null;
    }
}
