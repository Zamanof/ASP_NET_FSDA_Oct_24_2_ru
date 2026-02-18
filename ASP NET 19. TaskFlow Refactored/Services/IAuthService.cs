using ASP_NET_19._TaskFlow_Refactored.DTOs;

namespace ASP_NET_19._TaskFlow_Refactored.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest);
    Task RevokeRefreshTokenAsync(string refreshToken);
}
