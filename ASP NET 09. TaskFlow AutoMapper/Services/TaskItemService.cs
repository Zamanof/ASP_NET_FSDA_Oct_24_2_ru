using ASP_NET_09._TaskFlow_AutoMapper.Data;
using ASP_NET_09._TaskFlow_AutoMapper.DTOs.TaskItem_DTOs;
using ASP_NET_09._TaskFlow_AutoMapper.Models;
using ASP_NET_09._TaskFlow_AutoMapper.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_09._TaskFlow_AutoMapper.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDbContext _context;

    private readonly IMapper _mapper;

    public TaskItemService(TaskFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTask)
    {
        var projectExixts = await _context
                                        .Projects
                                        .AnyAsync(p => p.Id == createTask.ProjectId);

        if (!projectExixts)
            throw new 
                ArgumentException($"Project with ID {createTask.ProjectId} not found");

        var task = _mapper.Map<TaskItem>(createTask);


        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        await _context
            .Entry(task)
            .Reference(t => t.Project)
            .LoadAsync();

        return _mapper.Map<TaskItemResponseDto>(task);
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
        return _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);
    }

    public async Task<TaskItemResponseDto?> GetByIdAsync(int id)
    {
        var task = await _context
                         .TaskItems
                         .Include(t => t.Project)
                         .FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
            return null;

        return _mapper.Map<TaskItemResponseDto>(task);
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId)
    {
        var tasks = await _context
                            .TaskItems
                            .Include(t => t.Project)
                            .Where(t => t.ProjectId == projectId)
                            .ToListAsync();
        return _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);
    }

    public async Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTask)
    {
        var task = await _context
                            .TaskItems
                            .Include(t => t.Project)
                            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null) return null;

        _mapper.Map(updateTask, task);

        await _context.SaveChangesAsync();

        return _mapper.Map<TaskItemResponseDto>(task);
    }
}
