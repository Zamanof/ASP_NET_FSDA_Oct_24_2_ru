using ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.DTOs;
using ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Models;

namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto createDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateDto);
    Task<bool> DeleteAsync(int id);

}
