using BLL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProgrammingForum_ASPNETCore.Hubs;
using System.Globalization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//Connection to DB //
var connectionString = builder.Configuration.GetConnectionString("ProgrammingForumDbConnectionStr");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connectionString);
});

// Add AutoMapper //
builder.Services.AddAutoMapper(typeof(Program));
//Add WebOptimizer
builder.Services.AddWebOptimizer();

// Add Service from BLL //
builder.Services.AddScoped<IPost, PostService>();
builder.Services.AddScoped<ITopic, TopicService>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

// Authentication //
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    })
    .AddOpenIdConnect("google", options =>
    {
        options.Authority = "https://accounts.google.com";
        options.ClientId = builder.Configuration["Forum:GoogleClientId"];
        options.ClientSecret = builder.Configuration["Forum:GoogleClientSecret"];
        options.CallbackPath = "/auth";
    });

//
builder.Services.AddSignalR();

// Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[] { "en", "uk" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);


var app = builder.Build();

//Seed data //
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseWebOptimizer(); //
app.UseRequestLocalization(localizationOptions); //

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<PostHub>("/hubs/post"); //

app.Run();
