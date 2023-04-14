using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResponse>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginCommandHandler(
        IRepository<User> userRepository,
        IAuthenticationService authenticationRepository,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _authenticationRepository = authenticationRepository;
        _tokenGenerator = tokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTrackedAsync(
            new UserSpecification(command.Email),
            cancellationToken);

        var userLogIn = await _authenticationRepository.LoginAsync(command.Email, command.Password);
        
        if (user is null || !userLogIn)
        {
            return UserErrors.InvalidCredentials;
        }

        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResponse(token);
    }
}