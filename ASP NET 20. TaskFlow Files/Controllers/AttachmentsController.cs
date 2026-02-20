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

        var project = await _projectService.GetProjectEntityAsync(task.ProjectId);

        var authResult = await _authorizationService.AuthorizeAsync(User, project, "ProjectMemberOrHigher");

        if (file is null || file.Length == 0)
            return BadRequest();

        AttachmentResponseDto dto;

        await using var stream = file.OpenReadStream();
        dto = await _taskAttachmentService.UploadAsync(taskId, stream, file.FileName, file.ContentType, file.Length, UserId, cancellationToken);

        if (dto is null) return NotFound();

        return Ok(ApiResponse<AttachmentResponseDto>.SuccessResponse(dto, "File uplaoded"));

    }
}
