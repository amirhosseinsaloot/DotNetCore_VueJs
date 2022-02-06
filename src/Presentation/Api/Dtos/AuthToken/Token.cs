namespace Api.Dtos.AuthToken;

public class Token
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiresIn { get; set; }

    public string TokenType { get; set; } = null!;
}
