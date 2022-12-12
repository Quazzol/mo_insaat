namespace Backend.Middleware;

public static class JwtExtensions
{
    public static void UseJwtAuthentication(this IApplicationBuilder app)
    {
        // The ugly jwt workaround
        app.Use(
            next => async context =>
            {
                try
                {
                    await next(context);
                }
                catch
                {
                    // If the headers have already been sent, you can't replace the status code.
                    // In this case, re-throw the exception to close the connection.
                    if (context.Response.HasStarted || !context.Items.ContainsKey("jwt-workaround"))
                    {
                        throw;
                    }

                    var onFailure = (Action?)context.Items["jwt-workaround"];
                    onFailure!();
                }
            });

        app.UseAuthentication();
    }
}