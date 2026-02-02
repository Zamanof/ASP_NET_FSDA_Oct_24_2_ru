using ASP_NET_12._Authentification_and_Authorization.Models;

namespace ASP_NET_12._Authentification_and_Authorization.DTOs.TaskItem_DTOs;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.ToDo;
}
