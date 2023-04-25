using Application.Admins.Common.Errors;
using Application.Admins.Common.Specifications;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Models;
using Domain.Admins;
using MediatR;
using ErrorOr;

namespace Application.Admins.Authentication.Commands.SignIn;

public record SignInAdminCommand(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResponse>>;

public class SignInAdminCommandHandler : IRequestHandler<SignInAdminCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IRepository<Admin> _adminRepository;
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public SignInAdminCommandHandler(
        IRepository<Admin> adminRepository,
        IIdentityService identityService,
        IJwtTokenGenerator tokenGenerator)
    {
        _adminRepository = adminRepository;
        _identityService = identityService;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<ErrorOr<AuthenticationResponse>> Handle(SignInAdminCommand command, CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetTrackedAsync(
            new AdminSpecification(command.Email),
            cancellationToken);

        var loginSucceed = await _identityService.LoginAsync(command.Email, command.Password);
        
        if (admin is null || !loginSucceed)
        {
            return AdminErrors.InvalidCredentials;
        }

        var roles = await _identityService.GetRolesByUserAsync(admin.Email);

        var token = _tokenGenerator.GenerateToken(admin, roles);

        return new AuthenticationResponse(token);
    }
}