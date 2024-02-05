using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyJobBoard.API.Web.Business.SwaggerSchemaFilters;
using MyJobBoard.Application.Features.Documents.Queries.MyJobBoard.Application.Features.Documents.Queries;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Application.Services.Injector;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Infrastructure.Persistence.Repositories.Injector;
using System.Text.Json.Serialization;


const string SWAGGER_BASE_DEFINITION_NAME = "myjobboard";
var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<MyJobBoardBusinessDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyJobBoardDatabase")));


// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.SlidingExpiration = true;
});


builder.Services.AddAuthorization();

// IdentitySettings
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<MyJobBoardBusinessDbContext>()
    .AddDefaultTokenProviders();
    

// IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{

    //User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});


builder.Services.AddMediatR((config) =>
{
    config.RegisterServicesFromAssembly(typeof(GetDocumentsQuery).Assembly);
});


builder.Services.AddBusinessServices();

builder.Services.AddMyJobBoardRepositories();

builder.Services.AddControllers()
    .AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.WriteIndented = true;
    });


// Swagger UI Settings
var authEndpointsRelativePath = new[] { "register", "login", "refresh", "confirmEmail", "resendConfirmationEmail", "forgotPassword", "resetPassword", "manage/2fa", "manage/info", "checkSession","logout" };
Func<ApiDescription, bool> isAuth = apiDesc => authEndpointsRelativePath.Contains(apiDesc.RelativePath);
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = SWAGGER_BASE_DEFINITION_NAME,
        Version = "1.0"
    });
    options.SwaggerDoc("Auth", new OpenApiInfo
    {
        Title = "Authentification",
        Version = "1.0"
    });
    options.TagActionsBy(apiDescription =>
    {
        var relativePath = apiDescription.RelativePath;
        return isAuth(apiDescription) ? new[] { "Auth" } : new[] { relativePath.Split('/')[1] };
    });
    options.SwaggerDoc("authentification", new OpenApiInfo { Title = "API d'authentification", Version = "v1" });

    options.DocInclusionPredicate((docName, apiDesc) =>
     {
         bool apiFromAuth = isAuth(apiDesc);
         return docName == "authentification" ? apiFromAuth : !apiFromAuth;

     });


    options.SchemaFilter<DateFieldSchemaFilter>();
    options.UseInlineDefinitionsForEnums();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostDevelopment", builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });

});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowLocalhostDevelopment");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "myJobBoard API");
        options.SwaggerEndpoint("/swagger/authentification/swagger.json", "Authentification API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
// Add authentication middleware for token based authentication
app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        await context.AuthenticateAsync("Identity.Bearer");
        var accessToken = context.Request.Headers.Authorization;
        ITokenService tokenService = context.RequestServices.GetRequiredService<ITokenService>();
        if (string.IsNullOrWhiteSpace(accessToken) ||tokenService.IsTokenBlacklisted(accessToken!))
        {
            context.Response.StatusCode = 401;
            return;
        }
    }
    await next();
});
app.UseAuthorization();
app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.Run();
