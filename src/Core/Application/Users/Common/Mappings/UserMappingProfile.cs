using Application.Common.Models;
using Application.Users.Common.Models;
using AutoMapper;
using Domain.Users;
using Domain.Users.Entities;

namespace Application.Users.Common.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<ChildUser, ChildUserResponse>();
        CreateMap<PaginatedList<User>, PaginatedList<UserResponse>>();
    }
}