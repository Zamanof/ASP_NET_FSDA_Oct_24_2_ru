using ASP_NET_09._TaskFlow_Swagger_Documentation.DTOs;
using ASP_NET_09._TaskFlow_Swagger_Documentation.Models;

namespace ASP_NET_09._TaskFlow_Swagger_Documentation.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);

}
