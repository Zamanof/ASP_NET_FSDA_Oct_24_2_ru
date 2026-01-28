namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.DTOs.TaskItem_DTOs;

public class TaskItemQueryParams
{
    /// <summary>
    /// Page number for pagination (starts from 1).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Field name used for sorting (e.g. CreatedAt, Priority, Title).
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Sorting direction: asc or desc.
    /// </summary>
    public string? SortDirection { get; set; }

    /// <summary>
    /// Filter tasks by status (e.g. ToDo, InProgress, Done).
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Filter tasks by priority (e.g. Low, Medium, High).
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// Search text applied to task title and description.
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Filter tasks by project identifier.
    /// </summary>
    public int? ProjectId { get; set; }


    public void Validate()
    {
        if (Page < 1) Page = 1;

        if (PageSize < 1) PageSize = 10;

        if (PageSize > 100) PageSize = 100;

        if (string.IsNullOrWhiteSpace(SortDirection)) 
            SortDirection = "asc";
        
        SortDirection = SortDirection.ToLower();

        if (SortDirection != "asc" && SortDirection != "desc") 
            SortDirection = "asc";
    }
}
