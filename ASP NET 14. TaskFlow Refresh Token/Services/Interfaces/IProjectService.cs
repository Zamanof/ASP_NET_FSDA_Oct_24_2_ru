using ASP_NET_14._TaskFlow_Refresh_Token.DTOs;
using ASP_NET_14._TaskFlow_Refresh_Token.Models;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);

}
