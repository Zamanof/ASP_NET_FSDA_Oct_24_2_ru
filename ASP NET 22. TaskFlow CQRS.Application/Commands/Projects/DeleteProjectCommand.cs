using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Commands.Projects;

public record DeleteProjectCommand(int Id): IRequest<bool>;
