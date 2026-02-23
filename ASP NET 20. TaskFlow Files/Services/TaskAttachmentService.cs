using ASP_NET_20._TaskFlow_Files.Data;
using ASP_NET_20._TaskFlow_Files.DTOs;
using ASP_NET_20._TaskFlow_Files.Models;
using ASP_NET_20._TaskFlow_Files.Storage;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace ASP_NET_20._TaskFlow_Files.Services;

public class TaskAttachmentService: ITaskAttachmentService
{
    public const long MaxFileSizeBytes = 5 * 1024 * 1024;
    public static readonly string[] AllowedExtensions = {
        ".jpg",
        ".jpeg",
        ".png",
        ".pdf",
        ".txt",
        ".zip",
    };
    public static readonly string[] AllowedContentTypes = {
        "image/jpeg",
        "image/png",
        "text/plain",
        "application/pdf",
        "application/zip",
        "application/x-zip-compressed",
    };

    private readonly TaskFlowDbContext _context;
    private readonly IFileStorage _storage;

    public TaskAttachmentService(TaskFlowDbContext context, IFileStorage storage)
    {
        _context = context;
        _storage = storage;
    }

    public async Task<AttachmentResponseDto?> UploadAsync(
        int taskId, 
        Stream stream, 
        string originalFileName, 
        string contentType, 
        long length, 
        string userId, 
        CancellationToken cancellationToken = default)

    {
        if (length > MaxFileSizeBytes)
            throw new ArgumentException($"File size must not exceed {MaxFileSizeBytes / (1024 * 1024)}MB");

        var ext = Path.GetExtension(originalFileName)?.ToLower();

        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            throw new ArgumentException($"Allowed types: {string.Join(", ", AllowedExtensions)}");

        if (!AllowedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException($"Allowed content types: {string.Join(", ", AllowedContentTypes)}");

        var task = await _context.TaskItems.FindAsync([taskId], cancellationToken);

        if (task is null)
            return null;

        var folderKey = $"tasks/{taskId}";

        var info = await _storage.UploadAsync(stream, originalFileName, contentType, folderKey, cancellationToken);

        var attachment = new TaskAttachment
        {
            TaskItemId = taskId,
            OriginalFileName = originalFileName,
            StoredFileName = info.StoredFileName,
            Size = info.Size,
            ContentType = contentType,
            UploadedByUserId = userId,
            UploadedAt = DateTimeOffset.UtcNow
        };

        _context.TaskAttachments.Add(attachment);
        await _context.SaveChangesAsync();

        return new AttachmentResponseDto
        {
            Id = attachment.Id,
            TaskItemId = attachment.TaskItemId,
            OriginalFileName = attachment.OriginalFileName,
            ContentType = attachment.ContentType,
            Size = attachment.Size,
            UploadedByUserId = attachment.UploadedByUserId,
            UploadedAt = attachment.UploadedAt
        };

    }
    public async Task<(Stream stream, string fileName, string contentType)?> GetDownloadAsync(
        int attachmentId, CancellationToken cancellationToken = default)
    {
        var att = await _context.TaskAttachments.FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

        if (att is null)
            return null;

        var key = $"tasks/{att.TaskItemId}/{att.StoredFileName}";

        var stream = await _storage.OpenReadAsync(key, cancellationToken);

        return (stream, att.OriginalFileName, att.ContentType);

    }
    public async Task<bool> DeleteAsync(int attachmentId, CancellationToken cancellationToken = default)
    {
        var att = await _context.TaskAttachments.FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

        if (att is null)
            return false;
        var key = $"tasks/{att.TaskItemId}/{att.StoredFileName}";

        _context.TaskAttachments.Remove(att);
        await _context.SaveChangesAsync();

        await _storage.DeleteAsync(key, cancellationToken);

        return true;
    }
    public async Task<TaskAttachmentInfo?> GetAttachmentInfoAsync(int attachmentId, CancellationToken cancellationToken = default)
    {
        var att = await _context.TaskAttachments
                                .Include(a=> a.TaskItem)
                                .FirstOrDefaultAsync(a => a.Id == attachmentId, cancellationToken);

        if (att is null)
            return null;

        return new TaskAttachmentInfo
        {
            Id = att.Id,
            TaskItemId = att.TaskItemId,            
            UploadedByUserId = att.UploadedByUserId,
            ProjectId = att.TaskItem.ProjectId,
            StoredFileName = att.StoredFileName,
            StorageKey = $"tasks/{att.TaskItemId}/{att.StoredFileName}"
        };
    }
}
