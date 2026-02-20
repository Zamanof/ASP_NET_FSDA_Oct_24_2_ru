using ASP_NET_20._TaskFlow_Files.Models;
using TaskStatus = ASP_NET_20._TaskFlow_Files.Models.TaskStatus;

namespace ASP_NET_20._TaskFlow_Files.DTOs;

public class TaskItemResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }

}

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.ToDo;
}

public class TaskStatusUpdateDto
{
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
}