using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using MediatR;
using ErrorOr;

namespace Application.Users.Authentication.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<ErrorOr<UserResponse>>;

public class GetCurrentUserQueryHandler : 
    IRequestHandler<GetCurrentUserQuery, ErrorOr<UserResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IReadRepository<User> _readRepository;
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        ICurrentUserService currentUserService,
        IReadRepository<User> readRepository,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _readRepository = readRepository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<UserResponse>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            return UserErrors.NotFound;
        }

        var userId = _currentUserService.UserId;

        var user = await _readRepository.GetAsync(new UserSpecification(userId), cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }

        return _mapper.Map<UserResponse>(user);
    }
}