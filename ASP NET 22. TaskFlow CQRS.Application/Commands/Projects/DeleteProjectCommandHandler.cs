using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Commands.Projects;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.FindAsync(request.Id);
        if (project is null)
            return false;
        await _projectRepository.RemoveAsync(project);
        return true;
    }
}
