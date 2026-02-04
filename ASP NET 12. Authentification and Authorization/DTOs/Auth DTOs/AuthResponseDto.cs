namespace ASP_NET_12._Authentification_and_Authorization.DTOs.Auth_DTOs;

public class AuthResponseDto
{
    public string Email { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
