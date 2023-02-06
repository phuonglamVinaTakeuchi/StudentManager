using Microsoft.EntityFrameworkCore;
using StudentManager.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using StudentManager.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("StudentManagerContextConnection") ?? throw new InvalidOperationException("Connection string 'StudentManagerContextConnection' not found.");

builder.Services.AddDbContext<StudentManagerContext>(options => options.UseSqlServer(connectionString));

builder.Services
  .AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<StudentManagerContext>();

builder.Services.AddAuthorizationPolicies();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.RegisterRepositoryScoped();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
