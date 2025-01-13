using InstitudeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using static InstitudeManagementSystem.Controllers.AdminController;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();//for add Session-----
var provider = builder.Services.BuildServiceProvider(); //Create Service here
var config = provider.GetRequiredService<IConfiguration>();//Configure Service
builder.Services.AddDbContext<TeknowellContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
 
app.Run();
