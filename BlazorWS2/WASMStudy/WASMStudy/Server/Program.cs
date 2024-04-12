using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using WASMStudy.Server.DBContext;
using WASMStudy.Server.Services;
using WASMStudy.Server.Services.Interfaces;
using WASMStudy.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<YwdbContext>(options => options.UseSqlServer("Data Source=123.2.156.122,1002;Database=YWDB;User Id=stec;Password=stecdev1234!;TrustServerCertificate=true;"));

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
