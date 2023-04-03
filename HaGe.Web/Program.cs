using HaGe.Application.Interfaces;
using HaGe.Application.Services;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HaGeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<HaGeContext>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICodeListService, CodeListService>();

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ICodeListRepository, CodeListRepository>();
builder.Services.AddTransient<ICodeListValueRepository, CodeListValueRepository>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "UserScheme";
        options.DefaultChallengeScheme = "UserScheme";
    })
    .AddCookie("UserScheme", o => o.LoginPath = new PathString("/member/login"))
    .AddCookie("AdminScheme", o => o.LoginPath = new PathString("/admin/useraccount/login"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();

