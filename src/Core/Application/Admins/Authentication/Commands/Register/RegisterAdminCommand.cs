using Application.Admins.Common.Errors;
using Application.Admins.Common.Models;
using Application.Admins.Common.Specifications;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using AutoMapper;
using Domain.Admins;
using ErrorOr;
using MediatR;

namespace Application.Admins.Authentication.Commands.Register;

public record RegisterAdminCommand(
    string Email,
    string Password) : IRequest<ErrorOr<AdminResponse>>;
    
public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, ErrorOr<AdminResponse>>
{
    private readonly IRepository<Admin> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterAdminCommandHandler(
        IRepository<Admin> userRepository,
        IIdentityService identityService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<AdminResponse>> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetAsync(
            new AdminSpecification(command.Email),
            cancellationToken) is not null;

        if (userExists)
        {
            return AdminErrors.AlreadyExists;
        }

        var admin = Admin.Create(
            command.Email);
        
        _userRepository.Add(admin);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var registerSucceed = await _identityService.RegisterAsync(
            admin.Id,
            command.Email,
            command.Password,
            admin: true);

        if (!registerSucceed)
        {
            _userRepository.Delete(admin);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return AdminErrors.RegistrationFailed;
        }

        return _mapper.Map<AdminResponse>(admin);
    }
}