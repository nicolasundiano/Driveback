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
    private readonly IUpdateUserService _updateUserService;

    public UpdateCurrentUserCommandHandler(IUpdateUserService updateUserService)
    {
        _updateUserService = updateUserService;
    }

    public async Task<ErrorOr<UserResponse>> Handle(UpdateCurrentUserCommand command, CancellationToken cancellationToken)
    {
        return await _updateUserService.UpdateDetails(
            command.FirstName,
            command.LastName,
            command.Phone,
            default,
            cancellationToken);
    }
}