namespace Services.Jwt;

public class RefreshToken
{
    public string refresh_token { get; set; }

    public DateTime refresh_token_expires_in { get; set; }
}
