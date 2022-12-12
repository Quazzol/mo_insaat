using System.Security.Claims;
using Backend.DTOs;
using Backend.Misc;

namespace Backend.Middleware;

public class ConcurrentUserAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public ConcurrentUserAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (!httpContext.Items.ContainsKey("token-validated"))
        {
            // id authorization not required, continue with next step.
            await _next.Invoke(httpContext);
            return;
        }

        var userContext = (UserContext?)httpContext.RequestServices.GetService(typeof(IUserContext));
        var currentUser = userContext?.CurrentUser;
        if (currentUser != null)
        {
            httpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>
            {
                new Claim("Id", ClaimTypes.Name, currentUser.Id.ToString()),
                new Claim("UserType", ClaimTypes.Role, currentUser.Type.GetDescription())
            }));
        }

        await _next.Invoke(httpContext);
    }
}
