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
builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();
builder.Services.AddScoped<IWatchlistItemRepository, WatchlistItemRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IStreamAvailService, StreamAvailService>(s => new StreamAvailService(StreamAvailKey));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 6;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 3;

    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.MaxAge = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
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
