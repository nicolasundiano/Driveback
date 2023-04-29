using FluentValidation;

namespace Application.Users.ChildUsers.Commands.UpdateChildUser;

public class UpdateChildUserCommandValidator : AbstractValidator<UpdateChildUserCommand>
{
    public UpdateChildUserCommandValidator()
    {
        RuleFor(c => c.Property1).NotEmpty();
    }
}