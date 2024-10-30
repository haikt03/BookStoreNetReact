using BookStoreNetReact.Api.Extensions;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data.SeedData;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:5000"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var cloudUploadService = services.GetRequiredService<ICloudUploadService>();
    await context.Database.MigrateAsync();
    await AppSeeder.SeedDataAsync(context, userManager, cloudUploadService);
}
catch (Exception ex)
{
    Console.WriteLine("Seed data failed");
    throw new Exception(ex.ToString());
}

app.Run();
