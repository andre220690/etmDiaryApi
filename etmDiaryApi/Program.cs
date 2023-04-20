using etmDiaryApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string MySpecificOrigins = "_mySpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options
        .UseMySql("server=194.58.109.195;port=3306;user=root;password=123456ai;database=diary;",
                new MySqlServerVersion(new Version(8, 0, 25))));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MySpecificOrigins,
        builder =>
        {
                    builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.UseCors(MySpecificOrigins);

app.MapControllers();

app.Run();
