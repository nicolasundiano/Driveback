using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using ErrorOr;
using MediatR;

namespace Application.Users.Current.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<ErrorOr<UserResponse>>;

public class GetCurrentUserQueryHandler : 
    IRequestHandler<GetCurrentUserQuery, ErrorOr<UserResponse>>
{
    private readonly IUserService _userService;
    
    public GetCurrentUserQueryHandler(
        IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<ErrorOr<UserResponse>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        return await _userService.GetUserResponse(
            userId: default,
            cancellationToken: cancellationToken);
    }
}