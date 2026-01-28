using ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Models;

namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.DTOs.TaskItem_DTOs;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.ToDo;
}
