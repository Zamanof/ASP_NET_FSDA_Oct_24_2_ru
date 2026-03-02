using ASP_NET_22._TaskFlow_CQRS.Application.Commands.Projects;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using ASP_NET_22._TaskFlow_CQRS.Domain;
using FluentAssertions;
using Moq;

namespace ASP_NET_22._TaskFlow_CQRS_Unit_Test.Handler;

public class DeleteProjectCommandHandlerTests
{
    [Fact]
    public async Task Handle_ProjectExists_RemoveAndReturnsTrue()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();
        var project = new Project
        {
            Id = 1,
            Name = "proj1",
            OwnerId = "user1",
            CreatedAt = DateTimeOffset.UtcNow
        };

        projectRepo.Setup(r => r.FindAsync(1)).ReturnsAsync(project);

        var handler = new DeleteProjectCommandHandler(projectRepo.Object);
        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        projectRepo.Verify(r => r.RemoveAsync(project), Times.Once);
    }

    [Fact]
    public async Task Handle_ProjectNotFound_ReturnsFalse()
    {
        // Arrange
        var projectRepo = new Mock<IProjectRepository>();

        projectRepo.Setup(r => r.FindAsync(999)).ReturnsAsync((Project?)null);

        var handler = new DeleteProjectCommandHandler(projectRepo.Object);
        var command = new DeleteProjectCommand(999);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        projectRepo.Verify(r => r.RemoveAsync(It.IsAny<Project>()), Times.Never);
    }
}
