using ASP_NET_14._TaskFlow_Refresh_Token.Models;

namespace ASP_NET_14._TaskFlow_Refresh_Token.DTOs;
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
    
