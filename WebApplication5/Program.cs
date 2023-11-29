using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSqlServer<StackOverflowCloneContext>(Environment.GetEnvironmentVariable("SOC_CONN_STRING"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.ConfigureApplicationCookie(opt => { 
    opt.AccessDeniedPath = "/Error/Forbidden"; 
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

app.CreateDevDbIfNotExists();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error/ServerError");
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

    else if (ctx.Response.StatusCode == 403 && !ctx.Response.HasStarted) 
    {
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Error/Forbidden";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}");


app.Run();
