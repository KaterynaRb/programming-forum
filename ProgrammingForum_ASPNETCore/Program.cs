using BLL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProgrammingForum_ASPNETCore.Hubs;
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
builder.Services.AddControllersWithViews();

// Authentication //
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.Events = new CookieAuthenticationEvents()
        {
            OnSigningIn = async context =>
            {
                var principal = context.Principal;
                if (principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value == "christopher")
                {
                    var claimsIdentity = principal.Identity as ClaimsIdentity;
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }
            }
        };
    });

//
builder.Services.AddSignalR();

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
