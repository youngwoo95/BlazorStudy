using BlazorManager.Areas.Identity;
using BlazorManager.Authentication;
using BlazorManager.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

#region 사용자 인증
builder.Services.AddAuthenticationCore();
#endregion

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();


#region 사용자 인증
// 종속성 주입
builder.Services.AddScoped<ProtectedSessionStorage>();
// 맞춤 인증 상태 제공자 클래스에 대한 종속성 주입. -- 범위가 지정된 종속성
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
// 샘플 데이터
builder.Services.AddSingleton<UserAccountService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region 블레이저 전용 파일서버 생성

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider("N:\\개인"), // 실제경로
    RequestPath = "/System", // 호출할때 사용될 경로
    EnableDirectoryBrowsing = true // 필수옵션
});
#endregion

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
