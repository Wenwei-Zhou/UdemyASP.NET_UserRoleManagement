using RoleAndManagement.Data;
using RoleAndManagement.Contants;
using RoleAndManagement.Models;
using RoleAndManagement.Repositories;
using RoleAndManagement.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("RoleManagement"));
// });
// Microsoft SQL！！！！！！

builder.Services.Configure<MongoDBContext>(
//     options =>
// {
//     options.ConnectionString = builder.Configuration.GetConnectionString("mongo");
//     options.DatabaseName = builder.Configuration["MongoDBSettings:DatabaseName"];
//     options.JobCollection = builder.Configuration["MongoDBSettings:JobCollection"];
//     options.AuthicationCollection = builder.Configuration["MongoDBSettings:AuthicationCollection"];
// }
    builder.Configuration.GetSection("MongoDBSettings")
);

// 检查是否有cookie，添加 Cookie 认证
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// builder.Services.AddScoped<IRepository<JobPosting>, JobPostingRepository>();

builder.Services.AddScoped<IRepository<JobPosting>, MongoDBRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        // ASP.NET Core 默认对 POST/PUT/DELETE 请求启用了 Anti-forgery 验证（CSRF Token），以防止别人伪造表单来攻击你的接口。
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    RoleSeeder.SeedRolesAsync(services).Wait();
    UserSeeder.SeedUserAsync(services).Wait();
    
}

builder.Services.AddSingleton<UserService>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=JobPostings}/{action=Index}/{id?}");

app.Run();
