using ASP_NET_19._TaskFlow_Refactored.Common;
using ASP_NET_19._TaskFlow_Refactored.DTOs;
using ASP_NET_19._TaskFlow_Refactored.Models;

namespace ASP_NET_19._TaskFlow_Refactored.Services;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<PagedResult<TaskItemResponseDto>> GetPagedAsync(TaskItemQueryParams queryParams);
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItem?> GetTaskEntityAsync(int id);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTask);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTask);
    Task<TaskItemResponseDto?> UpdateStatusAsync(int id, TaskStatusUpdateDto taskStatus);
    Task<bool> DeleteAsync(int id);
}
