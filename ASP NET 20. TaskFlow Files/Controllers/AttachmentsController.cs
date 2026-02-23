using ASP_NET_20._TaskFlow_Files.Common;
using ASP_NET_20._TaskFlow_Files.DTOs;
using ASP_NET_20._TaskFlow_Files.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP_NET_20._TaskFlow_Files.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize (Policy ="UserOrAbove")]
public class AttachmentsController : ControllerBase
{
    private readonly ITaskAttachmentService _taskAttachmentService;
    private readonly IProjectService _projectService;
    private readonly ITaskItemService _taskItemService;
    private readonly IAuthorizationService _authorizationService;

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public AttachmentsController(
        ITaskAttachmentService taskAttachmentService,
        IProjectService projectService,
        IAuthorizationService authorizationService,
        ITaskItemService taskItemService)
    {
        _taskAttachmentService = taskAttachmentService;
        _projectService = projectService;
        _authorizationService = authorizationService;
        _taskItemService = taskItemService;
    }
    [HttpPost("~/api/tasks/{taskId}/attachments)")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<ApiResponse<AttachmentResponseDto>>> UploadAsync(
        int taskId, IFormFile file, CancellationToken cancellationToken
        )
    {
        var task = await _taskItemService.GetTaskEntityAsync(taskId);

        var project = await _projectService.GetProjectEntityAsync(task!.ProjectId);

        var authResult = await _authorizationService.AuthorizeAsync(User, project, "ProjectMemberOrHigher");

        if (!authResult.Succeeded)
            return Forbid();

        if (file is null || file.Length == 0)
            return BadRequest();

        AttachmentResponseDto? dto;

        await using var stream = file.OpenReadStream();

        dto = await _taskAttachmentService.UploadAsync(taskId, stream, file.FileName, file.ContentType, file.Length, UserId, cancellationToken);

        if (dto is null) 
            return NotFound();

        return Ok(ApiResponse<AttachmentResponseDto>.SuccessResponse(dto, "File uplaoded"));
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id, CancellationToken cancellationToken)
    {
        var info = await _taskAttachmentService.GetAttachmentInfoAsync(id, cancellationToken);
        
        if (info is null)
            return NotFound();

        var project = await _projectService.GetProjectEntityAsync(info.ProjectId);

        if (project is null)
            return NotFound();

        var authResult = await _authorizationService.AuthorizeAsync(User, project, "ProjectMemberOrHigher");
        if (!authResult.Succeeded)
            return Forbid();

        var result = await _taskAttachmentService.GetDownloadAsync(id, cancellationToken);
        if (result is null)
            return NotFound();

        return File(result.Value.stream,  result.Value.contentType, result.Value.fileName);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var info = await _taskAttachmentService.GetAttachmentInfoAsync(id, cancellationToken);

        if (info is null)
            return NotFound();

        var project = await _projectService.GetProjectEntityAsync(info.ProjectId);

        if (project is null)
            return NotFound();

        var authResult = await _authorizationService.AuthorizeAsync(User, project, "ProjectOwnerOrAdmin");
        if (!authResult.Succeeded)
            return Forbid();

        var deleted = await _taskAttachmentService.DeleteAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
