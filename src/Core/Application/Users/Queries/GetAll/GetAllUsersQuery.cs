using Application.Common.Extensions;
using Application.Common.Interfaces.Persistence;
using Application.Common.Models;
using Application.Users.Common.Models;
using Application.Users.Common.Specifications;
using AutoMapper;
using Domain.Users;
using MediatR;

namespace Application.Users.Queries.GetAll;

public record GetAllUsersQuery(
    string? Search,
    string? Sort,
    int? PageIndex,
    int? PageSize) : IRequest<PaginatedList<UserResponse>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserResponse>>
{
    private readonly IReadRepository<User> _readRepository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(
        IReadRepository<User> readRepository,
        IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return _mapper.Map<PaginatedList<UserResponse>>(
            await _readRepository.PaginatedListAsync(
                new UsersSpecification(query.Search, query.Sort, query.PageIndex, query.PageSize),
                cancellationToken));
    }
}