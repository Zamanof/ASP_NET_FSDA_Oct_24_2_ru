using ASP_NET_08._TaskFlow_DTOs.Models;

namespace ASP_NET_08._TaskFlow_DTOs.DTOs.TaskItem_DTOs;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.ToDo;
}
