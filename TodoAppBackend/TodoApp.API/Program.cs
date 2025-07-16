using Microsoft.EntityFrameworkCore;
using TodoApp.API.Mappings;
using TodoApp.Core.Services;
using TodoApp.Domain.Repositories;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddControllers();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddAutoMapper(typeof(TodoProfile));

// База данных
builder.Services.AddDbContext<TodoDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Миграции
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
	db.Database.Migrate();
}

app.Run();