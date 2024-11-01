using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using BookStoreNetReact.Application.Dtos.Image;
using static System.Net.Mime.MediaTypeNames;

namespace BookStoreNetReact.Infrastructure.Data.SeedData
{
    public class AppSeeder
    {
        public static async Task SeedDataAsync(AppDbContext context, UserManager<AppUser> userManager, ICloudUploadService cloudUploadService)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../BookStoreNetReact.Infrastructure/Data/SeedData"));

            if (!context.Categories.Any())
            {
                try
                {
                    var categoriesData = await File.ReadAllTextAsync(Path.Combine(basePath, "categories.json"));
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                    if (categories == null || categories.Count == 0)
                        return;

                    foreach (var category in categories)
                    {
                        context.Categories.Add(category);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Seed categories failed");
                    throw new Exception(ex.ToString());
                }
            }

            if (!context.Authors.Any())
            {
                try
                {
                    var authorsData = await File.ReadAllTextAsync(Path.Combine(basePath, "authors.json"));
                    var authorsSeeder = JsonSerializer.Deserialize<List<AuthorSeeder>>(authorsData);
                    if (authorsSeeder == null || authorsSeeder.Count == 0)
                        return;

                    foreach (var authorSeeder in authorsSeeder)
                    {
                        var author = new Author
                        {
                            FullName = authorSeeder.FullName,
                            Biography = authorSeeder.Biography,
                            Country = authorSeeder.Country
                        };

                        if (authorSeeder.ImagePath != null)
                        {
                            using (var fileStream = File.OpenRead(Path.Combine(basePath, @$"Images\Authors\{authorSeeder.ImagePath}")))
                            {
                                var uploadResult = await cloudUploadService.UploadImageAsync(new UploadImageDto
                                {
                                    FileStream = fileStream,
                                    FileName = authorSeeder.ImagePath
                                }, folder: "Authors");
                                if (uploadResult != null)
                                {
                                    author.PublicId = uploadResult.PublicId;
                                    author.ImageUrl = uploadResult.ImageUrl;
                                }
                            }
                        }
                        context.Authors.Add(author);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Seed authors failed");
                    throw new Exception(ex.ToString());
                }
            }

            if (!context.Books.Any())
            {
                try
                {
                    var booksData = await File.ReadAllTextAsync(Path.Combine(basePath, "books.json"));
                    var booksSeeder = JsonSerializer.Deserialize<List<BookSeeder>>(booksData);
                    if (booksSeeder == null || booksSeeder.Count == 0)
                        return;

                    foreach (var bookSeeder in booksSeeder)
                    {
                        var book = new Book
                        {
                            Name = bookSeeder.Name,
                            Translator = bookSeeder.Translator,
                            Publisher = bookSeeder.Publisher,
                            PublishedYear = bookSeeder.PublishedYear,
                            Language = bookSeeder.Language,
                            Weight = bookSeeder.Weight,
                            NumberOfPages = bookSeeder.NumberOfPages,
                            Form = bookSeeder.Form,
                            Description = bookSeeder.Description,
                            Price = bookSeeder.Price,
                            Discount = bookSeeder.Discount,
                            QuantityInStock = bookSeeder.QuantityInStock,
                            CategoryId = bookSeeder.CategoryId,
                            AuthorId = bookSeeder.AuthorId
                        };

                        if (bookSeeder.ImagePath != null)
                        {
                            using (var fileStream = File.OpenRead(Path.Combine(basePath, @$"Images\Books\{bookSeeder.ImagePath}")))
                            {
                                var uploadResult = await cloudUploadService.UploadImageAsync(new UploadImageDto
                                {
                                    FileStream = fileStream,
                                    FileName = bookSeeder.ImagePath
                                }, folder: "Books");
                                if (uploadResult != null)
                                {
                                    book.PublicId = uploadResult.PublicId;
                                    book.ImageUrl = uploadResult.ImageUrl;
                                }
                            }
                        }
                        context.Books.Add(book);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Seed books failed");
                    throw new Exception(ex.ToString());
                }
            }

            if (!userManager.Users.Any())
            {
                try
                {
                    var appUserData = await File.ReadAllTextAsync(Path.Combine(basePath, "appUsers.json"));
                    var appUsersSeeder = JsonSerializer.Deserialize<List<AppUserSeeder>>(appUserData);

                    if (appUsersSeeder == null || appUsersSeeder.Count == 0)
                        return;

                    foreach (var appUserSeeder in appUsersSeeder)
                    {
                        var appUser = new AppUser
                        {
                            UserName = appUserSeeder.UserName,
                            Email = appUserSeeder.Email,
                            PhoneNumber = appUserSeeder.PhoneNumber,
                            FirstName = appUserSeeder.FirstName,
                            LastName = appUserSeeder.LastName,
                            DateOfBirth = DateOnly.Parse(appUserSeeder.DateOfBirth.ToString()),
                        };

                        if (appUserSeeder.ImagePath != null)
                        {
                            using (var fileStream = File.OpenRead(Path.Combine(basePath, @$"Images\AppUsers\{appUserSeeder.ImagePath}")))
                            {
                                var uploadResult = await cloudUploadService.UploadImageAsync(new UploadImageDto
                                {
                                    FileStream = fileStream,
                                    FileName = appUserSeeder.ImagePath
                                }, folder: "AppUsers");
                                if (uploadResult != null)
                                {
                                    appUser.PublicId = uploadResult.PublicId;
                                    appUser.ImageUrl = uploadResult.ImageUrl;
                                }
                            }
                        }
                        if (appUserSeeder.Password != null)
                        {
                            var result = await userManager.CreateAsync(appUser, appUserSeeder.Password);

                            if (appUserSeeder.Role == "Admin" && result.Succeeded)
                                await userManager.AddToRolesAsync(appUser, new[] { "Member", "Admin" });

                            if (appUserSeeder.Role == "Member" && result.Succeeded)
                                await userManager.AddToRoleAsync(appUser, "Member");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Seed app users failed");
                    throw new Exception(ex.ToString());
                }
            }
        }

        public class AuthorSeeder : Author
        {
            public string? ImagePath { get; set; }
        }

        public class BookSeeder : Book
        {
            public string? ImagePath { get; set; }
        }

        public class AppUserSeeder : AppUser
        {
            public required string Password { get; set; }
            public string? ImagePath { get; set; }
            public required string Role { get; set; }
        }
    }
}
