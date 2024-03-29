using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSqlServer<StackOverflowCloneContext>(Environment.GetEnvironmentVariable("SOC_CONN_STRING"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder("Cookies").RequireAuthenticatedUser().Build();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// db context wrapper
builder.Services.AddScoped<SOCloneContextService>();

// business logic used by web controllers
builder.Services.AddScoped<AnswerBusinessController>();

builder.Services.AddScoped<HomeBusinessController>();

builder.Services.AddScoped<ProfileBusinessController>();

builder.Services.AddScoped<QuestionBusinessController>();

builder.Services.AddScoped<TagBusinessController>();

builder.Services.AddScoped<UserAuthorizer>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.MigrateSchema();
    app.UseExceptionHandler("/Error/ServerError");
} 
else 
{
    app.CreateDevDbIfNotExists();
}

app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Error/PageNotFound";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=LandingPage}");

app.Run();
