using ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Models;

namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.DTOs;
/// <summary>
/// DTO for create project. Uses for POST requestes
/// </summary>
public class CreateProjectDto
{
    /// <summary>
    /// Project name
    /// </summary>
    /// <example>My new project</example>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Project Description
    /// </summary>
    /// <example>Description for my project</example>
    public string Description { get; set; } = string.Empty;
}
    
