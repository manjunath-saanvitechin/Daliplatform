using Microsoft.EntityFrameworkCore;
using HandbookService.Data;
using HandbookService.Repositories;
using HandbookService.Services;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
builder.Services.AddDbContext<HandbookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IHandbookRepository, HandbookRepository>();
builder.Services.AddScoped<IHandbookManager, HandbookManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
