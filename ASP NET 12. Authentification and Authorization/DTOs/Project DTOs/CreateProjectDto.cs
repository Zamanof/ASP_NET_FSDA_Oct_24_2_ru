using ASP_NET_12._Authentification_and_Authorization.Models;

namespace ASP_NET_12._Authentification_and_Authorization.DTOs;
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
    
