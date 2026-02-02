using ASP_NET_12._Authentification_and_Authorization.DTOs;
using ASP_NET_12._Authentification_and_Authorization.Models;

namespace ASP_NET_12._Authentification_and_Authorization.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);

}
