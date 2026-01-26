using ASP_NET_09._TaskFlow_AutoMapper.DTOs.TaskItem_DTOs;
using ASP_NET_09._TaskFlow_AutoMapper.Models;

namespace ASP_NET_09._TaskFlow_AutoMapper.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTask);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTask);
    Task<bool> DeleteAsync(int id);
}
