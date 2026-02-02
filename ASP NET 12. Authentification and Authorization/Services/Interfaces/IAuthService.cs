using ASP_NET_12._Authentification_and_Authorization.DTOs.Auth_DTOs;

namespace ASP_NET_12._Authentification_and_Authorization.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}
