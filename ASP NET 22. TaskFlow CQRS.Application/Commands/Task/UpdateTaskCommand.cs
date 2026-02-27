using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Commands.Task;

public record UpdateTaskCommand(int Id, UpdateTaskItemDto UpdateTaskItem):IRequest<TaskItemResponseDto?>;

class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskItemResponseDto?>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskItemRepository.GetByIdWithProjectAsync(request.Id);
        if (task is null) return null;
        _mapper.Map(request.UpdateTaskItem, task);
        await _taskItemRepository.UpdateAsync(task);
        return _mapper.Map<TaskItemResponseDto>(task);
    }
}
