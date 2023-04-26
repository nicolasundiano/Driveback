using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    string? FirstName,
    string? LastName,
    string? Phone) : IRequest<ErrorOr<UserResponse>>;

public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UserResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCurrentUserCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<User> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UserResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        
        var user = await _repository.GetTrackedAsync(new UserSpecification(userId), cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.UpdateDetails(command.FirstName, command.LastName, command.Phone);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }
}