using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using MediatR;
using ErrorOr;

namespace Application.Users.Authentication.Commands.UpdateCurrentUser;

public record UpdateCurrentUserCommand(
    string? FirstName,
    string? LastName,
    string? Phone) : IRequest<ErrorOr<UserResponse>>;

public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand, ErrorOr<UserResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _readRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCurrentUserCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<User> readRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UserResponse>> Handle(UpdateCurrentUserCommand command, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            return UserErrors.NotFound;
        }
        
        var userId = _currentUserService.UserId;

        var user = await _readRepository.GetTrackedAsync(new UserSpecification(userId), cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.UpdateDetails(command.FirstName, command.LastName, command.Phone);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserResponse>(user);
    }
}