using Application.Users.Authentication.Common.Interfaces;
using Application.Users.Authentication.Common.Models;
using MediatR;

namespace Application.Users.Authentication.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<CurrentUserResponse>;

public class GetCurrentUserQueryHandler : 
    IRequestHandler<GetCurrentUserQuery, CurrentUserResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    
    public Task<CurrentUserResponse> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_currentUserService.GetCurrentUser());
    }
}