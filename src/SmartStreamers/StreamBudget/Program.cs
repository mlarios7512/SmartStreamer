using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StreamBudget.Models;
using StreamBudget.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using StreamBudget.Services.Abstract;
using StreamBudget.Services.Concrete;
using StreamBudget.DAL.Abstract;
using StreamBudget.DAL.Concrete;

var builder = WebApplication.CreateBuilder(args);

string StreamAvailKey = builder.Configuration["StreamAvailKey"];

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
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IStreamAvailService, StreamAvailService>(s => new StreamAvailService(StreamAvailKey));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();
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
