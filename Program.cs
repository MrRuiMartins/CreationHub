using CreationHub.Controllers;
using CreationHub.Models;
using CreationHub.Models.NicePartUsage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CreationHubContext>(opt =>
    opt.UseInMemoryDatabase("NicePartUsage"));
builder.Services.AddSingleton<IAzureBlobStorage, AzureBlobStrategy>();
    
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
