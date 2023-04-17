using Application.Common.Models;
using Application.Users.Common.Models;
using AutoMapper;
using Domain.Users;

namespace Application.Users.Common.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<PaginatedList<User>, PaginatedList<UserResponse>>();
    }
}