using Microsoft.EntityFrameworkCore;//Required for working with Entity 
using AnimalProject.Data;//Required for working with Context

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"]!;//Configuration
builder.Services.AddDbContext<AnimalContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();//Work with all the necessary services
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AnimalContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("Default", "{controller=Main}/{action=Home}/{id?}");

app.Run();