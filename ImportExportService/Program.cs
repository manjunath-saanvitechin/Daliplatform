// ImportExportService/Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); // Add Swagger if you want API documentation

// Add any other required services here, e.g., DbContext, repositories
// builder.Services.AddScoped<IExportService, ExportService>();
// builder.Services.AddScoped<IImportService, ImportService>();

var app = builder.Build();

// Configure the middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImportExportService v1"));
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
