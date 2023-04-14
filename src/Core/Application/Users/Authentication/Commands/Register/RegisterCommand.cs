using Application.Common.Interfaces.Persistence;
using Application.Users.Authentication.Common.Constants;
using Application.Users.Authentication.Common.Interfaces;
using Application.Users.Common.Errors;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string FirstName,
    string LastName,
    string Phone,
    string Password) : IRequest<ErrorOr<UserResponse>>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<UserResponse>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(
        IRepository<User> userRepository,
        IIdentityService identityService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<UserResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetAsync(
            new UserSpecification(command.Email),
            cancellationToken) is not null;

        if (userExists)
        {
            return UserErrors.AlreadyExists;
        }

        var user = User.Create(
            command.Email,
            command.FirstName,
            command.LastName,
            command.Phone);
        
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var registerSucceed = await _identityService.RegisterAsync(
            user.Id,
            command.Email,
            command.Password,
            UserRoles.Customer);

        if (!registerSucceed)
        {
            _userRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return UserErrors.RegistrationFailed;
        }

        return _mapper.Map<UserResponse>(user);
    }
}