using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using ErrorOr;

namespace Application.Users.Common.Services;

public class UpdateUserService : IUpdateUserService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _readRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserService(
        ICurrentUserService currentUserService,
        IRepository<User> readRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UserResponse>> UpdateDetails(
        string? firstName,
        string? lastName,
        string? phone,
        Guid userId = default,
        CancellationToken cancellationToken = default)
    {
        if (userId == default)
        {
            if (!_currentUserService.IsAuthenticated)
            {
                return UserErrors.NotFound;
            }
        
            userId = _currentUserService.UserId;
        }
       

        var user = await _readRepository.GetTrackedAsync(new UserSpecification(userId), cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.UpdateDetails(firstName, lastName, phone);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserResponse>(user);
    }
}