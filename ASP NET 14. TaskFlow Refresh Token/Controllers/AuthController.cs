using ASP_NET_14._TaskFlow_Refresh_Token.Common;
using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Auth_DTOs;
using ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody]RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);

        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "User registered successfully"));
    }
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody]LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);

        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successfully"));
    }
}
