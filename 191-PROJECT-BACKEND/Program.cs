using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _191_PROJECT_BACKEND.Data;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

var connectionStringProduct = builder.Configuration.GetConnectionString("ProductContext") ?? throw new InvalidOperationException("Connection string 'ProductContext' not found.");
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(connectionStringProduct));

var connectionString = builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContextConnection' not found.");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserContext>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//!!!!!!!!!!!!!!!!!!!!
app.MapRazorPages();

app.Run();
