using RoleAndManagement.Data;
using RoleAndManagement.Contants;
using RoleAndManagement.Models;
using RoleAndManagement.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("RoleManagement"));
// });
// Microsoft SQL！！！！！！

builder.Services.Configure<MongoDBContext>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("mongo");
    options.DatabaseName = builder.Configuration["MongoDBSettings:DatabaseName"];
    options.JobCollection = builder.Configuration["MongoDBSettings:JobCollection"];
    options.AuthicationCollection = builder.Configuration["MongoDBSettings:AuthicationCollection"];

});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IRepository<JobPosting>, JobPostingRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=JobPostings}/{action=Index}/{id?}");

app.Run();
