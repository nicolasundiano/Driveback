using Application.Users.Authentication.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Attributes;

public class MustHaveAdminRoleAttribute : AuthorizeAttribute
{
    public MustHaveAdminRoleAttribute() =>
        Roles = UserRoles.Admin;
}