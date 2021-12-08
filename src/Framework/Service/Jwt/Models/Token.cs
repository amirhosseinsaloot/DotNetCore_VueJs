using System.IdentityModel.Tokens.Jwt;

namespace Service.Jwt.Models;

public class Token
{
    #region Properties

    public string AccessToken { get; set; }

    public int AccessTokenExpiresIn { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiresIn { get; set; }

    public string TokenType { get; set; }


    #endregion Properties

    #region Ctor

    public Token(JwtSecurityToken securityToken, RefreshToken refreshToken)
    {
        AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        AccessTokenExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        RefreshToken = refreshToken.refresh_token;
        RefreshTokenExpiresIn = refreshToken.refresh_token_expires_in;
        TokenType = "Bearer";
    }

    #endregion Ctor
}
