namespace Service.Jwt.Models;

public class RefreshToken
{
    public string refresh_token { get; set; } = default!;

    public DateTime refresh_token_expires_in { get; set; }
}
