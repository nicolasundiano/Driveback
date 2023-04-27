using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using MediatR;
using ErrorOr;

namespace Application.Users.ChildUsers.Commands.UpdateChildUser;

public record UpdateChildUserCommand(
    Guid ChildUserId,
    string? Property1,
    int? Property2) : IRequest<ErrorOr<UserResponse>>;

public class UpdateChildUserCommandHandler : IRequestHandler<UpdateChildUserCommand, ErrorOr<UserResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateChildUserCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<UserResponse>> Handle(UpdateChildUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        
        var trackedUser = await _userRepository.GetTrackedAsync(new UserSpecification(userId, true), cancellationToken);

        if (trackedUser is null)
        {
            return UserErrors.NotFound;
        }

        if (trackedUser.GetChildUser(command.ChildUserId) is null)
        {
            return ChildUserErrors.NotFound;
        }
        
        trackedUser.UpdateChildUser(command.ChildUserId, command.Property1, command.Property2);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserResponse>(trackedUser);
    }
}