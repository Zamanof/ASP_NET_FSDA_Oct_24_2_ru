using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Commands.Projects;
public record UpdateProjectCommand(int Id, UpdateProjectDto UpdateProjectDto) : IRequest<ProjectResponseDto?>;