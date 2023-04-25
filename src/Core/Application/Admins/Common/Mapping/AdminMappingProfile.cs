using Application.Admins.Common.Models;
using AutoMapper;
using Domain.Admins;

namespace Application.Admins.Common.Mapping;

public class AdminMappingProfile : Profile
{
    public AdminMappingProfile()
    {
        CreateMap<Admin, AdminResponse>();
    }
}