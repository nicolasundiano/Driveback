using Infrastructure.Authentication.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Common.Attributes;

public class MustHaveAdminRoleAttribute : AuthorizeAttribute
{
    public MustHaveAdminRoleAttribute() =>
        Roles = UserRoles.Admin;
}