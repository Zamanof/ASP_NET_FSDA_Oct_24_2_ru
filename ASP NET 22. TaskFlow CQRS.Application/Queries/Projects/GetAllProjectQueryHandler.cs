using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Queries.Projects;

class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery, IEnumerable<ProjectResponseDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetAllProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectResponseDto>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllForUserAsync(request.UserId, request.Roles);
        return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
    }
}
