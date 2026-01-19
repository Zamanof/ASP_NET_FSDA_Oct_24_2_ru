using ASP_NET_07._TaskFlow_Introduction.Data;
using ASP_NET_07._TaskFlow_Introduction.Models;
using ASP_NET_07._TaskFlow_Introduction.Services.Interfaces;

namespace ASP_NET_07._TaskFlow_Introduction.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDbContext _context;

    public TaskItemService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public Task<TaskItem> CreateAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId)
    {
        throw new NotImplementedException();
    }

    public Task<TaskItem?> UpdateAsync(int id, Project project)
    {
        throw new NotImplementedException();
    }
}
