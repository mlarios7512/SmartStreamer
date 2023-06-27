using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StreamBudget.Models;
using StreamBudget.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
var SBconnectionString = builder.Configuration.GetConnectionString("SBConnection");
builder.Services.AddDbContext<SBDbContext>(
    options => options.UseLazyLoadingProxies().UseSqlServer(SBconnectionString)
);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AuthenticationConnection") 
    ?? throw new InvalidOperationException(
        "Connection string 'AuthenticationConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>options.UseSqlServer(connectionString)
    );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<DbContext, SBDbContext>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
