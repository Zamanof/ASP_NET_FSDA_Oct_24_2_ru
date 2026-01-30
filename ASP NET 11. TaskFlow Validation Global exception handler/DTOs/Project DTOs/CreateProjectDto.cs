using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Models;

namespace ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs;
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
    
