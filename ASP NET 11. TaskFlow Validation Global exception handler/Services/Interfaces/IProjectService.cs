using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Models;

namespace ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);

}
