using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using Domain.Users.Entities;
using MediatR;
using ErrorOr;

namespace Application.Users.Commands.AddChildUser;

public record AddChildUserCommand(
    string Property1,
    int Property2) : IRequest<ErrorOr<UserResponse>>;

public class AddChildUserCommandHandler : IRequestHandler<AddChildUserCommand, ErrorOr<UserResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddChildUserCommandHandler(
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
    
    public async Task<ErrorOr<UserResponse>> Handle(AddChildUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        
        var user = await _userRepository.GetTrackedAsync(new UserSpecification(userId), cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.AddChildUser(ChildUser.Create(command.Property1, command.Property2));
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }
}