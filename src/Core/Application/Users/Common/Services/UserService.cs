using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Errors;
using Application.Users.Common.Interfaces;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using Domain.Users.Entities;
using ErrorOr;

namespace Application.Users.Common.Services;

public class UserService : IUserService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(
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

    public async Task<ErrorOr<UserResponse>> UpdateDetails(
        string? firstName,
        string? lastName,
        string? phone,
        Guid userId = default,
        CancellationToken cancellationToken = default)
    {
        var user = await GetUserForUpdate(userId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.UpdateDetails(firstName, lastName, phone);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }

    public async Task<ErrorOr<UserResponse>> AddChildUser(
        ChildUser childUser,
        Guid userId = default,
        CancellationToken cancellationToken = default)
    {
        var user = await GetUserForUpdate(userId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        user.AddChildUser(childUser);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UserResponse>(user);
    }

    public async Task<ErrorOr<UserResponse>> GetUserResponse(
        Guid userId = default,
        CancellationToken cancellationToken = default)
    {
        userId = UserId(userId);

        var user = await _repository.GetAsync(new UserSpecification(userId), cancellationToken);
        
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        return _mapper.Map<UserResponse>(user);
    }

    private async Task<User?> GetUserForUpdate(Guid userId = default, CancellationToken cancellationToken = default)
    {
        userId = UserId(userId);

        return await _repository.GetTrackedAsync(new UserSpecification(userId), cancellationToken);
    }

    private Guid UserId(Guid userId = default)
    {
        if (userId != default)
        {
            return userId;
        }

        return _currentUserService.IsAuthenticated ? 
            _currentUserService.UserId : 
            Guid.Empty;
    }
}