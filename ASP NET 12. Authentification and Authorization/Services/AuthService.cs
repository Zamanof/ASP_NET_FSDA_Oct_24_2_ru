using ASP_NET_12._Authentification_and_Authorization.DTOs.Auth_DTOs;
using ASP_NET_12._Authentification_and_Authorization.Models;
using ASP_NET_12._Authentification_and_Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ASP_NET_12._Authentification_and_Authorization.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isValidPassword)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        return new AuthResponseDto
        {
            Email = user.Email!
        };

    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

        if(existingUser is not null)
        {
            throw new InvalidOperationException("User with this email already exists");
        }

        var user = new ApplicationUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }

        return new AuthResponseDto
        {
            Email = user.Email
        };
    }
}
