using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data.SeedData;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using BookStoreNetReact.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:5000"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Seed data
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
var cloudUploadService = scope.ServiceProvider.GetRequiredService<ICloudUploadService>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await AppSeeder.SeedDataAsync(context, userManager, cloudUploadService, logger);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while seeding data");
    throw;
}

app.Run();
