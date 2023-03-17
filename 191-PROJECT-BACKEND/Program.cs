using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _191_PROJECT_BACKEND.Data;
using Microsoft.Extensions.DependencyInjection;

//create builder
var builder = WebApplication.CreateBuilder(args);

//create db connections
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OrderContext") ?? throw new InvalidOperationException("Connection string 'OrderContext' not found.")));

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductContext") ?? throw new InvalidOperationException("Connection string 'ProductContext' not found.")));

var connectionString = builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContex' not found.");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite(connectionString));

//add signin requirement
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserContext>();

//add services to the container.
builder.Services.AddControllersWithViews();

//add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

//build the app
var app = builder.Build();

//use CORS
app.UseCors("CorsPolicy");

//use HTTPS redirections
app.UseHttpsRedirection();

//use files in wwwroot etc
app.UseStaticFiles();

//use routing
app.UseRouting();

//use authentication and authorization
app.UseAuthentication();;
app.UseAuthorization();

//map the controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductAdmin}/{action=Index}/{id?}");

//map the Razor pages
app.MapRazorPages();

//run the app
app.Run();
