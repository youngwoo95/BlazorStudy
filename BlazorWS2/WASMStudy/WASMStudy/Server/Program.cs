using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using WASMStudy.Server.DBContext;
using WASMStudy.Server.Hubs;
using WASMStudy.Server.Services;
using WASMStudy.Server.Services.Interfaces;
using WASMStudy.Shared;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<YwdbContext>(options => options.UseSqlServer("Data Source=123.2.156.122,1002;Database=YWDB;User Id=stec;Password=stecdev1234!;TrustServerCertificate=true;"));
builder.Services.AddDbContext<YwdbContext>(options => options.UseSqlServer("Data Source=127.0.0.1,1433;Database=YWDB;User Id=rladyddn258;Password=rladyddn!!95;TrustServerCertificate=true;"));


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 다른 도메인에서 들어오는 것을 허용

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5063",
                            "https://localhost:7166")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((host) => true);
    });
});

// SIGNAL R 서비스 등록
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
       new[] { "application/octet-stream" });
});

builder.Services.AddTransient<IUsersService, UsersService>();

var app = builder.Build();




// CORS 허용
app.UseCors();

// SIGNAL R 서비스 사용
app.UseResponseCompression();
app.MapHub<ChatHub>("/chathub");



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
