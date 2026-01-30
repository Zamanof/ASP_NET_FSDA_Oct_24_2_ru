using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Common;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Data;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs.TaskItem_DTOs;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Models;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskStatus = ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Models.TaskStatus;

namespace ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Services;

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

    public async Task<PagedResult<TaskItemResponseDto>> GetPagedAsync(TaskItemQueryParams queryParams)
    {
        queryParams.Validate();

        var query = _context
                          .TaskItems
                          .Include(t => t.Project)
                          .AsQueryable();

        if (queryParams.ProjectId.HasValue)
            query = query.Where(t => t.ProjectId == queryParams.ProjectId);

        if (!string.IsNullOrWhiteSpace(queryParams.Status))
        {
            if (Enum.TryParse<TaskStatus>(queryParams.Status, out var status))
            {
                query = query.Where(t => t.Status == status);
            }
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Priority))
        {
            if (Enum.TryParse<TaskPriority>(queryParams.Priority, out var priority))
            {
                query = query.Where(t => t.Priority == priority);
            }
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();

            query = query.Where(t => t.Title.ToLower().Contains(searchTerm) ||
                                t.Description.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Sort))
            query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection!);
        else
            query = query.OrderByDescending(t => t.CreatedAt);


        var totalCount = await query.CountAsync();


        var skip = (queryParams.Page - 1) * queryParams.PageSize;

        var tasks = await query
                            .Skip(skip)
                            .Take(queryParams.PageSize)
                            .ToListAsync();

        var tasksDtos = _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);

        return PagedResult<TaskItemResponseDto>.Create(
            tasksDtos,
            queryParams.Page,
            queryParams.PageSize,
            totalCount
            );
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

    private IQueryable<TaskItem> ApplySorting(
                                    IQueryable<TaskItem> query,
                                    string sortField,
                                    string sortDirection)
    {
        var isDescending = sortDirection?.ToLower() == "desc";

        return sortField.ToLower() switch
        {
            "title" => isDescending
            ? query.OrderByDescending(t=> t.Title)
            : query.OrderBy(t => t.Title),

            "createdat" => isDescending
            ? query.OrderByDescending(t => t.CreatedAt)
            : query.OrderBy(t => t.CreatedAt),

            "status" => isDescending
            ? query.OrderByDescending(t => t.Status)
            : query.OrderBy(t => t.Status),

            "priority" => isDescending
            ? query.OrderByDescending(t => t.Priority)
            : query.OrderBy(t => t.Priority),

            _ => query.OrderByDescending(t => t.CreatedAt)
        };
    }
}
