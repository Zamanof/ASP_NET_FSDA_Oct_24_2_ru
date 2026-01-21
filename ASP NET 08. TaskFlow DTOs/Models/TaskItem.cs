using System.Text.Json.Serialization;

namespace ASP_NET_08._TaskFlow_DTOs.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    // foreign key
    public int ProjectId { get; set; }

    [JsonIgnore]
    public virtual Project Project { get; set; } = null!;
}
public enum TaskStatus
{
    ToDo,
    InProgress,
    Done
}
