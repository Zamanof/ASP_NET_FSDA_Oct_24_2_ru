using ASP_NET_08._TaskFlow_DTOs.Data;
using ASP_NET_08._TaskFlow_DTOs.DTOs.TaskItem_DTOs;
using ASP_NET_08._TaskFlow_DTOs.Models;
using ASP_NET_08._TaskFlow_DTOs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_08._TaskFlow_DTOs.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDbContext _context;

    public TaskItemService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTask)
    {
        var projectExixts = await _context
                                        .Projects
                                        .AnyAsync(p => p.Id == createTask.ProjectId);

        if (!projectExixts)
            throw new 
                ArgumentException($"Project with ID {createTask.ProjectId} not found");

        var task = new TaskItem
        {
            Title = createTask.Title,
            Description = createTask.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            Status = Models.TaskStatus.ToDo,
            ProjectId = createTask.ProjectId,
        };
        

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        await _context
            .Entry(task)
            .Reference(t => t.Project)
            .LoadAsync();

        return MapToResponseDto(task);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task is null) return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
    {
        var tasks = await _context
                        .TaskItems
                        .Include(t => t.Project)
                        .ToListAsync();
        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskItemResponseDto?> GetByIdAsync(int id)
    {
        var task = await _context
                         .TaskItems
                         .Include(t => t.Project)
                         .FirstOrDefaultAsync(t => t.Id == id);
        return MapToResponseDto(task!);
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId)
    {
        var tasks = await _context
                            .TaskItems
                            .Include(t => t.Project)
                            .Where(t => t.ProjectId == projectId)
                            .ToListAsync();
        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTask)
    {
        var task = await _context
                            .TaskItems
                            .Include(t => t.Project)
                            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null) return null;

        task.Title = updateTask.Title;
        task.Description = updateTask.Description;
        task.Status = updateTask.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponseDto(task);
    }

    private TaskItemResponseDto MapToResponseDto(TaskItem taskItem)
    {
        return new TaskItemResponseDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Status = taskItem.Status.ToString(),
            ProjectId = taskItem.ProjectId,
            ProjectName = taskItem.Project.Name
        };
    }
}
