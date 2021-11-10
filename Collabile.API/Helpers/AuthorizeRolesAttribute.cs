using Collabile.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace Collabile.Api.Helpers;
public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles.Select(x => x.ToString()));
    }

}
