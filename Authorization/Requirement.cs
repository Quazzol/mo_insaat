using Microsoft.AspNetCore.Authorization;

namespace Backend.Authorization;

public class OnlyAdminRequirement : IAuthorizationRequirement
{ }

public class AtLeastModeratorRequirement : IAuthorizationRequirement
{ }