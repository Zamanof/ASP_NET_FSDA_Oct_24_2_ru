using ASP_NET_09._TaskFlow_AutoMapper.DTOs;
using ASP_NET_09._TaskFlow_AutoMapper.Models;

namespace ASP_NET_09._TaskFlow_AutoMapper.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);

}
