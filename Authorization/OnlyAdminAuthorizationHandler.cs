using Backend.Datas.Enums;
using Backend.Misc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Authorization;

public class OnlyAdminAuthorizationHandler : AuthorizationHandler<OnlyAdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyAdminRequirement requirement)
    {
        var claim = context.User.Claims.FirstOrDefault(q => q.Type.Equals("UserType"));
        if (claim != null && Enum.TryParse<UserType>(claim.ValueType, true, out var userType))
        {
            if (userType == UserType.Admin)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}