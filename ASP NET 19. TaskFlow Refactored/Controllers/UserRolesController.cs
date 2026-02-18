using ASP_NET_19._TaskFlow_Refactored.Common;
using ASP_NET_19._TaskFlow_Refactored.DTOs;
using ASP_NET_19._TaskFlow_Refactored.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_19._TaskFlow_Refactored.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AdminOnly")]
public class UserRolesController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRolesController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserWithRolesDto>>>> GetAll()
    {
        var users = _userManager
                            .Users
                            .OrderBy(u => u.Email)
                            .ToList();
        var usersWithRoles = new List<UserWithRolesDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            usersWithRoles.Add(new UserWithRolesDto
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList()
            });
        }
        return Ok(ApiResponse<IEnumerable<UserWithRolesDto>>.SuccessResponse(usersWithRoles, "List of users"));
    }

    [HttpGet("{userId}/roles")]
    public async Task<ActionResult<ApiResponse<UserWithRolesDto>>> GetRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return NotFound();

        var roles = await _userManager.GetRolesAsync(user);

        var userWithRoles = new UserWithRolesDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles.ToList()
        };
        return Ok(ApiResponse<UserWithRolesDto>.SuccessResponse(userWithRoles, "List of users"));
    }

    [HttpPost("{userId}/roles")]
    public async Task<ActionResult<ApiResponse<UserWithRolesDto>>> AssignRole(string userId, [FromBody] AssignRoleDto assignRole)
    {
        var roleName = assignRole.Role.Trim();

        if (string.IsNullOrEmpty(roleName))
            return BadRequest();

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return NotFound();

        if (!await _roleManager.RoleExistsAsync(roleName))
            return BadRequest();

        if (await _userManager.IsInRoleAsync(user, roleName))
            return BadRequest();

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded)
            return BadRequest();

        var roles = await _userManager.GetRolesAsync(user);

        var userWithRoles = new UserWithRolesDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles.ToList()
        };
        return Ok(ApiResponse<UserWithRolesDto>.SuccessResponse(userWithRoles, "User with role"));
    }

    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<ActionResult<ApiResponse<UserWithRolesDto>>> DeleteRole(string userId, string roleName)
    {
        roleName = roleName.Trim();

        if (string.IsNullOrEmpty(roleName))
            return BadRequest();

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return NotFound();

        if (!await _roleManager.RoleExistsAsync(roleName))
            return BadRequest();

        if (!await _userManager.IsInRoleAsync(user, roleName))
            return BadRequest();

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (!result.Succeeded)
            return BadRequest();

        var roles = await _userManager.GetRolesAsync(user);

        var userWithRoles = new UserWithRolesDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles.ToList()
        };
        return Ok(ApiResponse<UserWithRolesDto>.SuccessResponse(userWithRoles, "User with role"));
    }

}