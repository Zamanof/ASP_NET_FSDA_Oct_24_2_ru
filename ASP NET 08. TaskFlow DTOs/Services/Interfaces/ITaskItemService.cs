using ASP_NET_08._TaskFlow_DTOs.DTOs.TaskItem_DTOs;
using ASP_NET_08._TaskFlow_DTOs.Models;

namespace ASP_NET_08._TaskFlow_DTOs.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTask);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTask);
    Task<bool> DeleteAsync(int id);
}
