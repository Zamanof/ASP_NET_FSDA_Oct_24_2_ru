using ASP_NET_20._TaskFlow_Files.DTOs;
using ASP_NET_20._TaskFlow_Files.Models;
using FluentValidation;

namespace ASP_NET_20._TaskFlow_Files.Validators
{
    public class CreateTaskItemValidtor : AbstractValidator<CreateTaskItemDto>
    {
        public CreateTaskItemValidtor()
        {
            RuleFor(x=> x.Title)
                .NotEmpty().WithMessage("TaskItem Title is required")
                    .MinimumLength(3).WithMessage("TaskItem Title must be at least 3 characters long");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be greater than 0");

            RuleFor(x => x.Priority)
                .Must(p => new[] { TaskPriority.Low, TaskPriority.Medium, TaskPriority.High }.Contains(p))
                .WithMessage("Priority must be one of: 0(Low), 1(Medium), 2(High)");
        }
    }
}
