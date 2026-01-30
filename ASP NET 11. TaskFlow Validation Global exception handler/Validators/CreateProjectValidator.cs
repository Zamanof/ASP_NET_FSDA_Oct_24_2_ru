using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs;
using FluentValidation;

namespace ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Project Name is required")
                    .MinimumLength(3).WithMessage("Project Name must be at least 3 characters long");
    }
}
