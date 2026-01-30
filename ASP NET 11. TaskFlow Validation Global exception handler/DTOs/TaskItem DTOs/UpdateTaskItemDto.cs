using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Models;

namespace ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs.TaskItem_DTOs;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.ToDo;
}
