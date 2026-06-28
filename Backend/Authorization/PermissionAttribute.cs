using Microsoft.AspNetCore.Authorization;

namespace Backend.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = permission;
    }
}