using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Users.Current.Commands.UpdateCurrentUser;

public record UpdateCurrentUserCommand(
    string? FirstName,
    string? LastName,
    string? Phone) : IRequest<ErrorOr<UserResponse>>;

public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand, ErrorOr<UserResponse>>
{
    private readonly IGetUserService _getUserService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCurrentUserCommandHandler(
        IGetUserService getUserService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _getUserService = getUserService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UserResponse>> Handle(UpdateCurrentUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _getUserService.GetUser(default, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.UpdateDetails(command.FirstName, command.LastName, command.Phone);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }
}