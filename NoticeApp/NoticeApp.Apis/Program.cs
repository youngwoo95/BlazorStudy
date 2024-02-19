using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NoticeApp.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



#region CORS

// [CORS][1] CORS ��� ���
// [CORS][1][1] �⺻ : ��� ���
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", 
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// [CORS][1] CORS ��� ��ϰ� ����
// [CORS][1][2] ���� : ��� ���
builder.Services.AddCors(o => o.AddPolicy("AllowAllPolicy", options =>
{
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();

}));

// Ư���� �����θ� ���
// [CORS][1][3] ���� : Ư�� �����θ� ���
builder.Services.AddCors(o => o.AddPolicy("AllowSpecific", options => 
    options.WithOrigins("https://localhost:44356")
    .WithMethods("GET","POST","PUT","PATCH","DELETE")
    .WithHeaders("accept","content-type","origin","X-TotalRecordCount")
));

#endregion



// NoticeAppDbContext.cs Inject : New DbContext Add
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<NoticeAppDbContext>(options => options.UseSqlServer("Server=127.0.0.1,1433;database=NoticeApp;uid=rladyddn258;pwd=rladyddn!!95"));
/* ��������(NoticeApp) ���� ������(���Ӽ�) ���� ���� �ڵ常 ���� ��Ƽ� ���� */
// INoticeRepositoryAsync.cs Inject : DI Container�� ����(�������丮) ���
builder.Services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// [CORS][2] CORS ��� ���
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
