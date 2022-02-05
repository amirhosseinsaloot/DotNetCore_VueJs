using Core.Entities.Identity;
using Infrastructure.Dto.User;
using Infrastructure.Services.Jwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Jwt.Services;

public class JwtService
{
    private readonly ApplicationSettings _applicationSettings;

    private readonly SignInManager<User> _signInManager;

    private readonly ITenantDataProvider _tenantDataProvider;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IOptions<ApplicationSettings> settings, SignInManager<User> signInManager, ITenantDataProvider tenantDataProvider, IHttpContextAccessor httpContextAccessor)
    {
        _applicationSettings = settings.Value;
        _signInManager = signInManager;
        _tenantDataProvider = tenantDataProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Token> GenerateTokenAsync(User user, CancellationToken cancellationToken)
    {
        var secretKey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.SecretKey); // longer than 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(_applicationSettings.JwtSetting.EncryptKey); // Must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = await CreateClaimsAsync(user, cancellationToken);

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
        var refreshToken = new RefreshToken()
        {
            refresh_token = GenerateRefreshToken(),
            refresh_token_expires_in = DateTime.UtcNow.AddDays(_applicationSettings.JwtSetting.RefreshTokenExpirationDays)
        };

        return new Token(securityToken, refreshToken);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private async Task<IEnumerable<Claim>> CreateClaimsAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _signInManager.ClaimsFactory.CreateAsync(user);
        var role = await _signInManager.UserManager.GetRolesAsync(user);
        var tenant = await _tenantDataProvider.GetTenantByUserAsync(user.Id, cancellationToken);

        // Add custom claims
        var claims = new List<Claim>(result.Claims)
            {
                new Claim(nameof(CurrentUser.Id), user.Id.ToString()),
                new Claim(nameof(CurrentUser.Username), user.UserName),
                new Claim(nameof(CurrentUser.Firstname), user.Firstname),
                new Claim(nameof(CurrentUser.Lastname), user.Lastname),
                new Claim(nameof(CurrentUser.Email), user.Email),
                new Claim(nameof(CurrentUser.Birthdate), user.Birthdate.ToString()),
                new Claim(nameof(CurrentUser.PhoneNumber), user.PhoneNumber ?? ""),
                new Claim(nameof(CurrentUser.Gender), user.Gender.ToString()),
                new Claim(nameof(CurrentUser.Roles), string.Join(",",role)),
                new Claim(nameof(CurrentUser.TeamId), user.TeamId.ToString()),
                new Claim(nameof(CurrentUser.TenantId), tenant!.Id.ToString()),
            };

        return claims;
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

    public CurrentUser? GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return null;
        }

        return new CurrentUser
        {
            Id = int.Parse(user.FindFirst(nameof(CurrentUser.Id))!.Value),
            Username = user.FindFirst(nameof(CurrentUser.Username))!.Value,
            Firstname = user.FindFirst(nameof(CurrentUser.Firstname))!.Value,
            Lastname = user.FindFirst(nameof(CurrentUser.Lastname))!.Value,
            Email = user.FindFirst(nameof(CurrentUser.Email))!.Value,
            Birthdate = DateTime.Parse(user.FindFirst(nameof(CurrentUser.Birthdate))!.Value),
            PhoneNumber = user.FindFirst(nameof(CurrentUser.PhoneNumber))!.Value,
            Gender = Enum.Parse<GenderType>(user.FindFirst(nameof(CurrentUser.Gender))!.Value),
            Roles = user.FindFirst(nameof(CurrentUser.Roles))!.Value.Split(',').ToList(),
            TeamId = int.Parse(user.FindFirst(nameof(CurrentUser.TeamId))!.Value),
            TenantId = int.Parse(user.FindFirst(nameof(CurrentUser.TenantId))!.Value),
        };
    }
}