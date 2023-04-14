using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Common;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Register;

public record RegisterCommand(
    string Email,
    string FirstName,
    string LastName,
    string Phone,
    string Password) : IRequest<ErrorOr<UserResponse>>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<UserResponse>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(
        IRepository<User> userRepository,
        IAuthenticationService authenticationRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _authenticationRepository = authenticationRepository;
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

        var registerSucceed = await _authenticationRepository.RegisterAsync(
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