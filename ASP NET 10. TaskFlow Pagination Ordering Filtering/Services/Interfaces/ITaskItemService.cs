using ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Common;
using ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.DTOs.TaskItem_DTOs;

namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Services.Interfaces;

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
