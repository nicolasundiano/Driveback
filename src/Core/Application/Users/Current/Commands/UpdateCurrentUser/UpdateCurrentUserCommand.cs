using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using ErrorOr;
using MediatR;

namespace Application.Users.Current.Commands.UpdateCurrentUser;

public record UpdateCurrentUserCommand(
    string? FirstName,
    string? LastName,
    string? Phone) : IRequest<ErrorOr<UserResponse>>;

public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand, ErrorOr<UserResponse>>
{
    private readonly IUserService _userService;

    public UpdateCurrentUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ErrorOr<UserResponse>> Handle(UpdateCurrentUserCommand command, CancellationToken cancellationToken)
    {
        return await _userService.UpdateDetails(
            command.FirstName,
            command.LastName,
            command.Phone,
            default,
            cancellationToken);
    }
}