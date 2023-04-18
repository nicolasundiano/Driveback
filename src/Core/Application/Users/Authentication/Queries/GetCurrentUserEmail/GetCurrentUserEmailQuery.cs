using Application.Users.Authentication.Common.Interfaces;
using Application.Users.Authentication.Common.Models;
using MediatR;

namespace Application.Users.Authentication.Queries.GetCurrentUserEmail;

public record GetCurrentUserEmailQuery : IRequest<CurrentUserResponse>;

public class GetCurrentUserEmailQueryHandler : 
    IRequestHandler<GetCurrentUserEmailQuery, CurrentUserResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserEmailQueryHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    
    public Task<CurrentUserResponse> Handle(
        GetCurrentUserEmailQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_currentUserService.GetCurrentUser());
    }
}