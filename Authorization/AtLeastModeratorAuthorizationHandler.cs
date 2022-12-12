using Backend.Datas.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Authorization;

public class AtLeastModeratorAuthorizationHandler : AuthorizationHandler<AtLeastModeratorRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtLeastModeratorRequirement requirement)
    {
        var claim = context.User.Claims.FirstOrDefault(q => q.Type.Equals("UserType"));
        if (claim != null && Enum.TryParse<UserType>(claim.ValueType, true, out var userType))
        {
            if (userType == UserType.Admin || userType == UserType.Moderator)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}