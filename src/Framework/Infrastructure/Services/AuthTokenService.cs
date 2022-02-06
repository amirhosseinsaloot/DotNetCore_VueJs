using Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly ApplicationSettings _applicationSettings;

    public AuthTokenService(IOptions<ApplicationSettings> settings)
    {
        _applicationSettings = settings.Value;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.SecretKey); // longer than 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.EncryptKey); // Must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var tokenOptions = new SecurityTokenDescriptor
        {
            Issuer = _applicationSettings.JwtSetting.Issuer,
            Audience = _applicationSettings.JwtSetting.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(_applicationSettings.JwtSetting.NotBeforeMinutes),
            Expires = DateTime.UtcNow.AddDays(_applicationSettings.JwtSetting.AccessTokenExpirationDays),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(tokenOptions);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var secretKey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.SecretKey); // longer than 16 character
        var encryptionkey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.EncryptKey); // Must be 16 character

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
            ValidAudience = _applicationSettings.JwtSetting.Audience,
            ValidateIssuer = true,
            ValidIssuer = _applicationSettings.JwtSetting.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateLifetime = false, // Here we are saying that we don't care about the token's expiration date
            TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes128KW, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}