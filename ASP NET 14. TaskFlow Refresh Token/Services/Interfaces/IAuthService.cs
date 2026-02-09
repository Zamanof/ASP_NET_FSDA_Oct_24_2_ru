using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Auth_DTOs;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest);
    Task RevokeRefreshTokenAsync(string refreshToken);
}
