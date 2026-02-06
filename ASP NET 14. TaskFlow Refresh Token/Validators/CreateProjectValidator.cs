using ASP_NET_14._TaskFlow_Refresh_Token.DTOs;
using FluentValidation;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Project Name is required")
                    .MinimumLength(3).WithMessage("Project Name must be at least 3 characters long");
    }
}
