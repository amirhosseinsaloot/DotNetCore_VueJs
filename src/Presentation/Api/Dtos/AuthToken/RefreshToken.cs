namespace Api.Dtos.AuthToken;

public class RefreshToken
{
    public string refresh_token { get; set; } = null!;

    public DateTime refresh_token_expires_in { get; set; }
}
