using ASP_NET_09._TaskFlow_Swagger_Documentation.Common;
using ASP_NET_09._TaskFlow_Swagger_Documentation.DTOs;
using ASP_NET_09._TaskFlow_Swagger_Documentation.Models;
using ASP_NET_09._TaskFlow_Swagger_Documentation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASP_NET_09._TaskFlow_Swagger_Documentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <returns>List of all projects.</returns>
    /// <response code="200">Returns the list of projects successfully.</response>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProjectResponseDto>>>> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<ProjectResponseDto>>.SuccessResponse(projects, "Returns the list of projects successfully."));
    }

    /// <summary>
    /// Retrieves a project by its specific identifier.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <returns>The project with the specified ID.</returns>
    /// <response code="200">Returns the project if found.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> GetById(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project is null)
            return NotFound(ApiResponse<ProjectResponseDto>.ErrorResponse($"Project with ID {id} not found"));
        return Ok(ApiResponse<ProjectResponseDto>.SuccessResponse(project, "Project found."));
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="createProjectDto">Project data to create.</param>
    /// <returns>The created project.</returns>
    /// <response code="201">Returns the newly created project.</response>
    /// <response code="400">If the model is invalid.</response>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> Create([FromBody] CreateProjectDto createProjectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<ProjectResponseDto>.ErrorResponse("Invalid model state."));

        var createdProject = await _projectService.CreateAsync(createProjectDto);
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdProject.Id },
            ApiResponse<ProjectResponseDto>.SuccessResponse(createdProject, "Project created successfully."));
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <param name="updateProjectDto">Updated project data.</param>
    /// <returns>The updated project.</returns>
    /// <response code="200">Returns the updated project.</response>
    /// <response code="400">If the model is invalid.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> Update(int id, [FromBody] UpdateProjectDto updateProjectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<ProjectResponseDto>.ErrorResponse("Invalid model state."));

        var updatedProject = await _projectService.UpdateAsync(id, updateProjectDto);

        if (updatedProject is null)
            return NotFound(ApiResponse<ProjectResponseDto>.ErrorResponse($"Project with ID {id} not found"));

        return Ok(ApiResponse<ProjectResponseDto>.SuccessResponse(updatedProject, "Project updated successfully."));
    }

    /// <summary>
    /// Deletes a project by its identifier.
    /// </summary>
    /// <param name="id">Project identifier.</param>
    /// <returns>No content if deleted.</returns>
    /// <response code="204">Project deleted successfully.</response>
    /// <response code="404">If the project is not found.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var isDeleted = await _projectService.DeleteAsync(id);

        if (!isDeleted)
            return NotFound(ApiResponse<object>.ErrorResponse($"Project with ID {id} not found"));

        return Ok(ApiResponse<object>.SuccessResponse(null, "Project deleted successfully."));
    }
}
