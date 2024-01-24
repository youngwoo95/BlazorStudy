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

#region ����� ����
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


#region ����� ����
// ���Ӽ� ����
builder.Services.AddScoped<ProtectedSessionStorage>();
// ���� ���� ���� ������ Ŭ������ ���� ���Ӽ� ����. -- ������ ������ ���Ӽ�
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
// ���� ������
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

#region ������ ���� ���ϼ��� ����

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider("N:\\����"), // �������
    RequestPath = "/System", // ȣ���Ҷ� ���� ���
    EnableDirectoryBrowsing = true // �ʼ��ɼ�
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
