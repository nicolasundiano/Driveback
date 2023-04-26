using Infrastructure.Authentication.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Common.Attributes;

public class MustHaveUserRoleAttribute : AuthorizeAttribute
{
    public MustHaveUserRoleAttribute() =>
        Roles = UserRoles.User;
}