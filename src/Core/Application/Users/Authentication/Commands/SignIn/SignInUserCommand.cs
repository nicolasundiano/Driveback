using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Models;
using Application.Users.Common.Errors;
using Application.Users.Common.Specifications;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Authentication.Commands.SignIn;

public record SignInUserCommand(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResponse>>;

public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public SignInUserCommandHandler(
        IRepository<User> userRepository,
        IIdentityService identityService,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        SignInUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTrackedAsync(
            new UserSpecification(command.Email),
            cancellationToken);

        var loginSucceed = await _identityService.LoginAsync(command.Email, command.Password);
        
        if (user is null || !loginSucceed)
        {
            return UserErrors.InvalidCredentials;
        }

        var roles = await _identityService.GetRolesByUserAsync(user.Email);

        var token = _tokenGenerator.GenerateToken(user, roles);

        return new AuthenticationResponse(token);
    }
}