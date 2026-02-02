using ASP_NET_12._Authentification_and_Authorization.Common;
using ASP_NET_12._Authentification_and_Authorization.DTOs.TaskItem_DTOs;

namespace ASP_NET_12._Authentification_and_Authorization.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<PagedResult<TaskItemResponseDto>> GetPagedAsync(TaskItemQueryParams queryParams);
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTask);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTask);
    Task<bool> DeleteAsync(int id);
}
