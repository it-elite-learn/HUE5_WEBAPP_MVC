using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using App.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibContext") ?? throw new InvalidOperationException("Connection string 'LibContext' not found.")));

// identity
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContext' not found.")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
