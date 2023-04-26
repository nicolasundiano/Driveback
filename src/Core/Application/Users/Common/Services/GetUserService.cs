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

public class GetUserService : IGetUserService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _readRepository;

    public GetUserService(
        ICurrentUserService currentUserService,
        IRepository<User> readRepository)
    {
        _currentUserService = currentUserService;
        _readRepository = readRepository;
    }
    
    public async Task<User?> GetUser(Guid userId = default, CancellationToken cancellationToken = default)
    {
        if (userId == default)
        {
            if (!_currentUserService.IsAuthenticated)
            {
                return null;
            }
        
            userId = _currentUserService.UserId;
        }

        return await _readRepository.GetTrackedAsync(new UserSpecification(userId), cancellationToken);
    }
}