using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Backend.Misc;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;

internal class TokenProviderService : ITokenProviderService
{
    public const string TokenKey = "cr34t3d_t0k3n_k3y_<d0**23l32*oo!2<>q23333211";

    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenProviderService(IDateTimeProvider dateTimeProvider)
    {
        Check.NotNull(dateTimeProvider, nameof(dateTimeProvider));

        _dateTimeProvider = dateTimeProvider;
    }

    internal static SecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey));

    public (string token, DateTime validTo) CreateToken(UserModel? user)
    {
        if (user is null)
            return (string.Empty, DateTime.MinValue);

        var jwtToken = GetJwtSecurityToken();

        return (new JwtSecurityTokenHandler().WriteToken(jwtToken), jwtToken.ValidTo);

        JwtSecurityToken GetJwtSecurityToken()
        {
            var token = new JwtSecurityToken(
                expires: _dateTimeProvider.UtcNow.AddDays(90),
                signingCredentials: new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256)
            )
            {
                Payload =
                {
                    ["User"] = user
                }
            };

            return token;
        }
    }
}