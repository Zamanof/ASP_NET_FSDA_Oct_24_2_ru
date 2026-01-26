using ASP_NET_09._TaskFlow_Swagger_Documentation.Models;

namespace ASP_NET_09._TaskFlow_Swagger_Documentation.DTOs.TaskItem_DTOs;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Models.TaskStatus Status { get; set; } = Models.TaskStatus.ToDo;
}
