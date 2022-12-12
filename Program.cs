using Backend.Authorization;
using Backend.Connection;
using Backend.DTOs;
using Backend.DTOs.Response;
using Backend.Middleware;
using Backend.Misc;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>(x => new DefaultDateTimeProvider(() => DateTime.Now));
builder.Services.AddSingleton<IAuthorizationHandler, OnlyAdminAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AtLeastModeratorAuthorizationHandler>();

builder.Services.AddScoped<IImageLibraryRepository, ImageLibraryRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddScoped<IImageLibraryService, ImageLibraryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ITokenProviderService, TokenProviderService>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = TokenProviderService.GetSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async c =>
                    {
                        c.HttpContext.Items["jwt-workaround"] = new Action(
                            async () =>
                            {
                                //Log exception or reason 
                                c.Response.ContentType = "plain/text";
                                c.Response.StatusCode = StatusCodes.Status401Unauthorized;

                                const string authFailedMessage = "An error occurred processing your authentication.";
                                c.Response.Headers["auth-failed"] = new StringValues(authFailedMessage);

                                await c.Response.WriteAsync(authFailedMessage);
                            });

                        //c.Fail("An error occurred processing your authentication.");
                        await Task.CompletedTask;
                    },
                    OnTokenValidated = async t =>
                    {
                        t.HttpContext.Items["token-validated"] = true;

                        var payload = t.Principal!.Claims.FirstOrDefault(x => string.Compare(x.Type, "User", StringComparison.InvariantCultureIgnoreCase) == 0);
                        if (payload != null)
                        {
                            var user = JsonConvert.DeserializeObject<UserDTO>(payload.Value);
                            ((UserContext?)t.HttpContext.RequestServices.GetService<IUserContext>())!.CurrentUser = user;
                        }

                        await Task.CompletedTask;
                    }
                };
            });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.OnlyAdmins, policy => policy.Requirements.Add(new OnlyAdminRequirement()));
    o.AddPolicy(Policies.AtLeastModerators, policy => policy.Requirements.Add(new AtLeastModeratorRequirement()));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseJwtAuthentication();
app.UseConcurrentUserAuthorization();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
