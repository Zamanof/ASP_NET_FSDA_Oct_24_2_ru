using ASP_NET_19._TaskFlow_Refactored.DTOs;
using ASP_NET_19._TaskFlow_Refactored.Models;

namespace ASP_NET_19._TaskFlow_Refactored.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllForUserAsync(
        string userId, 
        IList<string> roles);
    Task<Project?> GetProjectEntityAsync(int id);
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(
        CreateProjectDto createDto, 
        string ownerId);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ProjectMemberResponse>> GetMembersAsync(int projectId);
    Task<IEnumerable<AvailableUserDto>> GetAvailableUsersToAddAsync(int projectId);
    Task<bool> AddMemberAsync(int projectId, string userIdOrEmail);
    Task<bool> RemoveMemberAsync(int projectId, string userId);

}
